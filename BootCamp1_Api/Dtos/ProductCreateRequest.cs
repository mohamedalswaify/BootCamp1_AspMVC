namespace BootCamp1_Api.Dtos
{
    public class ProductCreateRequest
    {
        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public int Qty { get; set; }

        public int CategoryId { get; set; }

        public string? Description { get; set; }

        public IFormFile? Image { get; set; }
    }

}
