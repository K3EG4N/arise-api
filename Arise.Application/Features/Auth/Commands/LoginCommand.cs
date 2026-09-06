using Arise.Application.Common.Results;
using MediatR;

namespace Arise.Application.Features.Auth.Commands
{
    public class LoginCommand : IRequest<Result<LoginResponse>>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
