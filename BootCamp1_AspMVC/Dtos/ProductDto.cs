using BootCamp1_AspMVC.Models;

namespace BootCamp1_AspMVC.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public int Qty { get; set; }

        public string CategoryName{ get; set; }

        public string? Description { get; set; }
    }
}
