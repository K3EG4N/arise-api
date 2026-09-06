using Arise.Domain.Entities;

namespace Arise.Application.Interfaces
{
    public interface ITokenGenerator
    {
        string Generate(User user, List<string> roles);
    }
}
