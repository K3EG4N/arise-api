namespace arise_api.dtos.Responses
{
    public class EmployeeByUserId
    {
        public Guid EmployeeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Foto { get; set; }
    }
}
