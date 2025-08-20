

using System.ComponentModel.DataAnnotations;

namespace BootCamp1_AspMVC.Models
{
    public class Product
    {

        [Key]
        public int Id { get; set; }

        public string ProductName { get; set; } 

        public decimal Price { get; set; }

        public int Qty { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public string? Description { get; set; }

    }
}
