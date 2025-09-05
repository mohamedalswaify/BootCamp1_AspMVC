using System.ComponentModel.DataAnnotations;

namespace BootCamp1_Api.Dtos
{
    public class LoginRequestDto
    {
        [Required] 
        public string Username { get; set; }
        [Required] 
        public string Password { get; set; }
    }
}
