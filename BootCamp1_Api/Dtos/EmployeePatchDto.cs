namespace BootCamp1_Api.Dtos
{
    public class EmployeePatchDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Username { get; set; }
        public bool? Islock { get; set; }
        public int? DepartmentId { get; set; }
    }
}
