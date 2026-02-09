using arise_api.dtos.Responses;
using arise_api.entities;
using arise_api.repositories;

namespace arise_api.services
{
    public interface IEmployeeService
    {
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
