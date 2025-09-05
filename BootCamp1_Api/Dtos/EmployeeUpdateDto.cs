using System.ComponentModel.DataAnnotations;

namespace BootCamp1_Api.Dtos
{
    public class EmployeeUpdateDto
    {
        [Required] public int Id { get; set; }
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }
        [Required, EmailAddress] public string Email { get; set; }
        [Required] public string Phone { get; set; }
        public string Address { get; set; } = "";
        [Required] public string Username { get; set; }
        public bool Islock { get; set; } = false;
        public int? DepartmentId { get; set; }
        // ملاحظة: لا نمرّر Password هنا إلا لو عملت DTO منفصل لتغيير كلمة المرور
    }
}
