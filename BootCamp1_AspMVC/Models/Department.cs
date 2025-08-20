using System.ComponentModel.DataAnnotations;

namespace BootCamp1_AspMVC.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
