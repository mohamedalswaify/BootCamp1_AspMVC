using System.ComponentModel.DataAnnotations;

namespace BootCamp1_AspMVC.Models
{
    public class Suplier
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }


        public string Phone { get; set; }
    }
}
