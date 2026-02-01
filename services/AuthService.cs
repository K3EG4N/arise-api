using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using arise_api.dtos.Generics;
using arise_api.dtos.Request;
using arise_api.dtos.Responses;
using arise_api.Entities;
using arise_api.helpers;
using Microsoft.IdentityModel.Tokens;

namespace arise_api.services
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
        Task<BaseResponse> RegisterAsync(RegisterRequest request);
    }

    public class AuthService(IUserService userService, IConfiguration configuration) : IAuthService
    {
        private readonly IUserService _userService = userService;
        private readonly IConfiguration _configuration = configuration;

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userService.FindUserByEmailAsync(request.Email);

            if (user == null)
                throw new Exception("Invalid credentials");

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                throw new Exception("Invalid credentials");

            var token = GenerateJwtToken(user);

            return new LoginResponse { Token = token };
        }

        public async Task<BaseResponse> RegisterAsync(RegisterRequest request)
        {
            var createUserRequest = new CreateUserRequest
            {
                Email = request.Email,
                Password = request.Password
            };

            return await _userService.CreateUserAsync(createUserRequest);
        }

        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? ""));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                // new Claim(ClaimTypes.Name, user.Username?? ""),
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTimeHelper.GetDateTimeNow().AddHours(24),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
