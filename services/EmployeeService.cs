using arise_api.dtos.Generics;
using arise_api.dtos.Request;
using arise_api.dtos.Responses;
using arise_api.entities;
using arise_api.generic;
using arise_api.helpers;
using arise_api.repositories;
using Microsoft.EntityFrameworkCore;

namespace arise_api.services
{
    public interface IEmployeeService
    {
        Task<DataGroup<ListEmployeeResponse>> GetAllEmployeesAsync(BaseFilter filter);
        Task<EmployeeByUserId?> GetEmployeeByUserId(Guid UserId);
        Task<BaseResponse> CreateEmployeeAsync(CreateEmployeeRequest request);
    }

    public class EmployeeService(IEmployeeRepository repository, IEmployeeStatusRepository status, IDepartmentRepository department, IBlobStorageService storage) : IEmployeeService
    {
        private readonly IEmployeeStatusRepository _status = status;
        private readonly IEmployeeRepository _repository = repository;
        private readonly IDepartmentRepository _department = department;
        private readonly IBlobStorageService _storage = storage;

        public async Task<EmployeeByUserId?> GetEmployeeByUserId(Guid UserId)
        {
            var employee = await _repository.FindEmployeeByUserIdAsync(UserId);

            if (employee == null)
                return null;

            return new EmployeeByUserId
            {
                EmployeeId = employee.EmployeeId,
                Name = BuildFullName(employee),
                Email = employee.User?.Email ?? "",
                Foto = employee.Photo
            };
        }

        public async Task<DataGroup<ListEmployeeResponse>> GetAllEmployeesAsync(BaseFilter filter)
        {
            var query = filter.Query?.Trim()?.ToLower();

            var words = (query ?? "").ToLower()
                        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                        .ToList();

            var employees = await _repository.GetAllAsync(new()
            {
                Predicate = q => string.IsNullOrEmpty(query) || q.Dni.Contains(query) || q.Code.Contains(query) ||
                                 words.All(word =>
                                     q.FirstName.ToLower().Contains(word) ||
                                     (q.MiddleName != null && q.MiddleName.ToLower().Contains(word)) ||
                                     q.PaternalLastName.ToLower().Contains(word) ||
                                     (q.MaternalLastName != null && q.MaternalLastName.ToLower().Contains(word))
                                 ),
                OrderBy = q => q.OrderBy(e => e.Code),
                Include = q => q.Include(e => e.User).Include(x => x.Status).Include(x => x.Department),
                Limit = filter.Limit,
                Offset = filter.Offset
            });

            var total = await _repository.CountAsync(x => string.IsNullOrEmpty(query) || x.Dni.Contains(query) || x.Code.Contains(query) ||
                                 words.All(word =>
                                     x.FirstName.ToLower().Contains(word) ||
                                     (x.MiddleName != null && x.MiddleName.ToLower().Contains(word)) ||
                                     x.PaternalLastName.ToLower().Contains(word) ||
                                     (x.MaternalLastName != null && x.MaternalLastName.ToLower().Contains(word))
                                 ));

            return new DataGroup<ListEmployeeResponse>
            {
                Data = [.. employees.Select(e => new ListEmployeeResponse
                {
                    Name = BuildFullName(e),
                    Email = e.User?.Email ?? "",
                    Photo = e.Photo,
                    Dni = e.Dni,
                    Code = e.Code,
                    Gender = e.Gender == Gender.Male ? "Male" : "Female",
                    Phone = e.Phone ?? string.Empty,
                    Department = e.Department != null ? e.Department.Name : string.Empty,
                    BirthDate = DateTimeHelper.FormatDateToString(e.BirthDate),
                    HireDate = DateTimeHelper.FormatDateToString(e.HireDate),
                    Status = e.Status.Name,
                    StatusCode = e.Status.Code
                })],
                CurrentPage = PaginationHelper.GetCurrentPage(filter.Offset, filter.Limit),
                TotalItems = total,
                TotalPages = PaginationHelper.GetTotalPages(total, filter.Limit)
            };
        }

        public async Task<BaseResponse> CreateEmployeeAsync(CreateEmployeeRequest request)
        {
            if (request == null)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Request body is required",
                    StatusCode = 400
                };
            }

            if (string.IsNullOrEmpty(request.Name))
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Name is required",
                    StatusCode = 400
                };
            }

            if (string.IsNullOrEmpty(request.LastName))
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Last name is required",
                    StatusCode = 400
                };
            }

            if (string.IsNullOrEmpty(request.Dni))
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Dni is required",
                    StatusCode = 400
                };
            }

            if (string.IsNullOrEmpty(request.BirthDate))
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Birth date is required",
                    StatusCode = 400
                };
            }

            if (request.Phone != null && request.Phone.Length > 9)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Phone number cannot exceed 9 characters",
                    StatusCode = 400
                };
            }

            if (request.Gender != Gender.Male && request.Gender != Gender.Female && request.Gender != Gender.Other)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Invalid gender. Please select either Male, Female, or Other.",
                    StatusCode = 400
                };
            }

            var birthDate = DateTimeHelper.ParseStringToDate(request.BirthDate);

            if (birthDate == DateTime.MinValue)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Invalid birth date format. Expected format: yyyy-MM-dd",
                    StatusCode = 400
                };
            }

            var existingEmployee = await _repository.ExistsAsync(e => e.Dni == request.Dni);

            if (existingEmployee)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "An employee with the same DNI already exists",
                    StatusCode = 400
                };
            }

            var activeStatus = await _status.GetFirstAsync(s => s.Code == EMPLOYEE_STATUS_CODE.ACTIVE);

            if (activeStatus == null)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Active status not found",
                    StatusCode = 404
                };
            }

            var department = await _department.GetFirstAsync(d => d.DepartmentId == request.DepartmentId);

            if (department == null)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Department not found",
                    StatusCode = 404
                };
            }

            Employee employee = new()
            {
                EmployeeId = Guid.NewGuid(),
                Dni = request.Dni,
                BirthDate = birthDate,
                Code = BuildCode(request.Dni),
                Phone = request.Phone,
                HireDate = DateTimeHelper.GetDateTimeNow(),
                StatusId = activeStatus.EmployeeStatusId,
                DepartmentId = department.DepartmentId
            };

            if (request.File != null && request.File.FileData != null && !string.IsNullOrEmpty(request.File.Extension))
            {
                var url = await _storage.UploadAsync(request.File, employee.EmployeeId);
                employee.Photo = url;
            }

            var (firstName, middleName) = SplitFullName(request.Name);
            employee.FirstName = firstName;
            employee.MiddleName = middleName;

            var (paternalLastName, maternalLastName) = SplitFullName(request.LastName);
            employee.PaternalLastName = paternalLastName;
            employee.MaternalLastName = maternalLastName;

            await _repository.AddAsync(employee);

            return new BaseResponse
            {
                Success = true,
                Message = "Employee created successfully"
            };
        }

        private static string BuildFullName(Employee employee)
        {
            var nameParts = new List<string>
            {
                employee.FirstName,
                employee.MiddleName!,
                employee.PaternalLastName,
                employee.MaternalLastName!
            };

            return string.Join(" ", nameParts.Where(x => !string.IsNullOrWhiteSpace(x)));
        }

        private static string BuildCode(string dni)
        {
            return new string([.. Convert.ToInt32(dni).ToString("X").Reverse()]);
        }

        private static (string Primary, string? Secondary) SplitFullName(string fullName)
        {
            if (fullName.Split(" ").Length > 1)
            {
                var parts = fullName.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                return (parts[0], string.Join(" ", parts.Skip(1)));
            }
            else
            {
                return (fullName, null);
            }
        }
    }
}
