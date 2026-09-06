namespace Arise.Application.Features.Employees.Queries
{
    public class GetEmployeeByUserIdResponse
    {
        public Guid EmployeeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? Photo { get; set; }
    }
}
