namespace arise_api.entities
{
    public class EmployeeStatus
    {
        public Guid EmployeeStatusId { get; set; }

        public string Code { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Color { get; set; } = string.Empty;
    }
}
