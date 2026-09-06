namespace Arise.Application.Interfaces
{
    public interface ICurrentUserService
    {
        Guid? UserId { get; }
        IEnumerable<string> Roles { get; }
    }
}
