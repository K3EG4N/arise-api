using arise_api.entities;
using arise_api.generic;
using arise_api.provider;
using Microsoft.EntityFrameworkCore;

namespace arise_api.repositories
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        Task<Employee?> FindEmployeeByUserIdAsync(Guid UserId);
        // Task<List<Employee>> GetAllEmployeesAsync();
    }

    public class EmployeeRepository(AriseDbContext context) : BaseRepository<Employee>(context), IEmployeeRepository
    {
        public async Task<Employee?> FindEmployeeByUserIdAsync(Guid UserId)
        {
            return await _context.Employees.Include(u => u.User).FirstOrDefaultAsync(e => e.UserId == UserId && e.DeletedAt == null);
        }

        // public async Task<List<Employee>> GetAllEmployeesAsync()
        // {
        //     return await _context.Employees.Include(u => u.User).ToListAsync();
        // }
    }
}
