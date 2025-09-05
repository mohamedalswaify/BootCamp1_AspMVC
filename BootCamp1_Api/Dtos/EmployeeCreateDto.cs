using System.ComponentModel.DataAnnotations;

namespace BootCamp1_Api.Dtos
{
    public class EmployeeCreateDto
    {
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }

        [Required, EmailAddress] public string Email { get; set; }
        [Required] public string Phone { get; set; }
        public string Address { get; set; } = "";

        [Required] public string Username { get; set; }
        [Required] public string Password { get; set; }

        public bool Islock { get; set; } = false;

        public int? DepartmentId { get; set; }
    }
}
