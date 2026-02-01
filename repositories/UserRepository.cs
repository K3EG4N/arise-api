using arise_api.Entities;
using arise_api.generic;
using arise_api.provider;

namespace arise_api.repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        // Task<User?> GetByEmailAsync(string email);
    }

    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AriseDbContext context) : base(context) { }

        // public async Task<User?> GetByEmailAsync(string email)
        // {
        //     return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        // }
    }
}
