namespace arise_api.dtos.Responses
{
    public class ListEmployeeResponse
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Photo { get; set; }
        public string Code { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string Dni { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string HireDate { get; set; } = null!;
        public string BirthDate { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string StatusCode { get; set; } = null!;
    }
}
