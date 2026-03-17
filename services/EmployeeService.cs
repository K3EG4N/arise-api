using arise_api.dtos.Responses;
using arise_api.entities;
using arise_api.generic;
using arise_api.helpers;
using arise_api.repositories;

namespace arise_api.services
{
    public interface IEmployeeService
    {
        Task<DataGroup<ListEmployeeResponse>> GetAllEmployeesAsync();
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

        public async Task<DataGroup<ListEmployeeResponse>> GetAllEmployeesAsync()
        {
            var employees = await _repository.GetAllEmployeesAsync();
            var total = await _repository.CountAsync(x => true);

            Thread.Sleep(2000);

            return new DataGroup<ListEmployeeResponse>
            {
                Data = [.. employees.OrderBy(x => x.FirstName).Select(e => new ListEmployeeResponse
                {
                    Name = BuildFullName(e),
                    Email = e.User.Email,
                    Phote = e.Photo,
                    Code = e.Code,
                    Phone = e.Phone ?? string.Empty,
                    BirthDate = DateTimeHelper.FormatDateToString(e.BirthDate),
                    HireDate = DateTimeHelper.FormatDateToString(e.HireDate),
                    Status = e.DeletedAt != null ? "Inactive" : "Active"
                })],
                CurrentPage = PaginationHelper.GetCurrentPage(0, 50),
                TotalItems = total,
                TotalPages = PaginationHelper.GetTotalPages(total, 50)
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
