using arise_api.Entities;
using arise_api.generic;
using arise_api.provider;
using Microsoft.EntityFrameworkCore;

namespace arise_api.repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> FindUserByEmailAsync(string email);
        Task<User?> FindUserByIdAsync(Guid id);
        Task<List<User>> GetAllUsersAsync();
    }

    public class UserRepository(AriseDbContext context) : BaseRepository<User>(context), IUserRepository
    {
        public async Task<User?> FindUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.DeletedAt == null && x.Email == email);
        }

        public async Task<User?> FindUserByIdAsync(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.DeletedAt == null && x.UserId == id);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.Where(x => x.DeletedAt == null).ToListAsync();
        }
    }
}
