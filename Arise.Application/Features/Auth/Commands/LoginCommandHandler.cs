using Arise.Application.Common.Enums;
using Arise.Application.Common.Results;
using Arise.Application.Interfaces;
using Arise.Domain.Entities;
using MediatR;

namespace Arise.Application.Features.Auth.Commands
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoginResponse>>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<UserRole> _userRoleRepository;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IPasswordHasher _passwordHasher;

        public LoginCommandHandler(
            IRepository<User> userRepository,
            ITokenGenerator tokenGenerator,
            IRepository<UserRole> userRoleRepository,
            IPasswordHasher passwordHasher
        )
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _tokenGenerator = tokenGenerator;
            _passwordHasher = passwordHasher;
        }

        public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FirstOrDefaultAsync(u => u.Email == request.Email && u.DeletedAt == null);

            if (user == null || !_passwordHasher.Verify(request.Password, user.Password))
            {
                return Result<LoginResponse>.Failure("Email o contraseña incorrectos", ErrorType.Unauthorized);
            }

            var userRoles = await _userRoleRepository.GetAllAsync(x => x.UserId == user.UserId, includes: x => x.Role);

            return Result<LoginResponse>.Success(new LoginResponse
            {
                Token = _tokenGenerator.Generate(user, [.. userRoles.Select(x => x.Role.Code)])
            });
        }
    }
}
