namespace arise_api.entities
{
    public class EmployeeStatus
    {
        public Guid EmployeeStatusId { get; set; }

        public string Code { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Color { get; set; } = string.Empty;
    }

    public static class EMPLOYEE_STATUS_CODE
    {
        public const string ACTIVE = "01",
                            INACTIVE = "02";
    }
}
