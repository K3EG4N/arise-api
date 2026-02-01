using arise_api.generic;
using arise_api.Entities;
using arise_api.provider;
using arise_api.dtos.Responses;
using arise_api.dtos.Request;
using arise_api.dtos.Generics;
using arise_api.helpers;
using System.Linq.Expressions;

namespace arise_api.services
{
    public interface IUserService : IBaseRepository<User>
    {
        Task<List<UserListResponse>> GetAllUsersAsync();
        Task<UserByIdResponse?> FindUserByIdAsync(Guid id);
        Task<BaseResponse> CreateUserAsync(CreateUserRequest request);
        Task<BaseResponse> UpdateUserAsync(Guid? id, UpdateUserRequest request);
        Task<BaseResponse> DeleteUserAsync(Guid? id);
    }

    public class UserService(AriseDbContext context) : BaseRepository<User>(context), IUserService
    {
        public async Task<List<UserListResponse>> GetAllUsersAsync()
        {
            Expression<Func<User, bool>> predicate = u => u.DeletedAt == null;

            var users = await GetAllAsync(predicate);

            return [.. users.Select(u => new UserListResponse
            {
                UserId = u.UserId,
                Username = u.Username,
                Email = u.Email
            })];
        }

        public async Task<UserByIdResponse?> FindUserByIdAsync(Guid id)
        {
            Expression<Func<User, bool>> predicate = u => u.DeletedAt == null
                && u.UserId == id;

            var user = await GetFirstAsync(predicate);

            if (user == null)
                return null;

            return new UserByIdResponse
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email
            };
        }

        public async Task<BaseResponse> CreateUserAsync(CreateUserRequest request)
        {
            if (request.Email == null)
            {
                return new BaseResponse
                {
                    Message = "Email is required",
                    Success = false
                };
            }

            if (request.Password == null)
            {
                return new BaseResponse
                {
                    Message = "Password is required",
                    Success = false
                };
            }

            bool userExists = await UserExistsAsync(request.Email);

            if (userExists)
            {
                return new BaseResponse
                {
                    Message = "User already exists",
                    Success = false
                };
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User
            {
                UserId = Guid.NewGuid(),
                Username = request.Username,
                Email = request.Email,
                Password = passwordHash
            };

            await AddAsync(user);

            return new BaseResponse
            {
                Message = "User created successfully",
                Success = true
            };
        }

        public async Task<BaseResponse> UpdateUserAsync(Guid? id, UpdateUserRequest request)
        {
            if (id == null)
            {
                return new BaseResponse
                {
                    Message = "UserId is required",
                    Success = false
                };
            }

            if (request.Email == null && request.Username == null && request.Password == null)
            {
                return new BaseResponse
                {
                    Message = "At least one field (Email, Username, Password) must be provided to update",
                    Success = false
                };
            }

            Expression<Func<User, bool>> predicate = u => u.DeletedAt == null
                && u.UserId == id;

            var user = await GetFirstAsync(predicate);

            if (user == null)
            {
                return new BaseResponse
                {
                    Message = "User not found",
                    Success = false
                };
            }

            if (request.Email != null)
            {
                user.Email = request.Email;
            }

            if (request.Username != null)
            {
                user.Username = request.Username;
            }

            if (request.Password != null)
            {
                var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
                user.Password = passwordHash;
            }

            await UpdateAsync(user);

            return new BaseResponse
            {
                Message = "User updated successfully",
                Success = true
            };
        }

        public async Task<BaseResponse> DeleteUserAsync(Guid? id)
        {
            if (id == null)
            {
                return new BaseResponse
                {
                    Message = "UserId is required",
                    Success = false
                };
            }

            Expression<Func<User, bool>> predicate = u => u.DeletedAt == null
                && u.UserId == id;

            var user = await GetFirstAsync(predicate);

            if (user == null)
            {
                return new BaseResponse
                {
                    Message = "User not found",
                    Success = false
                };
            }

            user.DeletedAt = DateTimeHelper.GetDateTimeNow();

            await UpdateAsync(user);

            return new BaseResponse
            {
                Message = "User deleted successfully",
                Success = true
            };
        }

        private async Task<bool> UserExistsAsync(string email)
        {
            Expression<Func<User, bool>> predicate = u => u.DeletedAt == null
                && (u.Email == email || u.Username == email);

            var user = await GetFirstAsync(predicate);

            return user != null;
        }
    }
}
