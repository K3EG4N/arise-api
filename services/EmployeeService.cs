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
    }

    public class EmployeeService(IEmployeeRepository repository) : IEmployeeService
    {
        private readonly IEmployeeRepository _repository = repository;

        public async Task<EmployeeByUserId?> GetEmployeeByUserId(Guid UserId)
        {
            var employee = await _repository.FindEmployeeByUserIdAsync(UserId);

            if (employee == null)
                return null;

            return new EmployeeByUserId
            {
                EmployeeId = employee.EmployeeId,
                Name = BuildFullName(employee),
                Email = employee.User.Email,
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
                Predicate = q => !string.IsNullOrEmpty(query) ?
                                 q.Dni.Contains(query) ||
                                 words.All(word =>
                                     q.FirstName.ToLower().Contains(word) ||
                                     (q.MiddleName != null && q.MiddleName.ToLower().Contains(word)) ||
                                     q.PaternalLastName.ToLower().Contains(word) ||
                                     (q.MaternalLastName != null && q.MaternalLastName.ToLower().Contains(word))
                                 ) : true,
                OrderBy = q => q.OrderBy(e => e.Code),
                Include = q => q.Include(e => e.User).Include(x => x.Status),
                Limit = filter.Limit,
                Offset = filter.Offset
            });
            var total = await _repository.CountAsync(x => true);

            return new DataGroup<ListEmployeeResponse>
            {
                Data = [.. employees.Select(e => new ListEmployeeResponse
                {
                    Name = BuildFullName(e),
                    Email = e.User.Email,
                    Photo = e.Photo,
                    Dni = e.Dni,
                    Code = e.Code,
                    Gender = e.Gender == Gender.Male ? "Male" : "Female",
                    Phone = e.Phone ?? string.Empty,
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
    }
}
