using BootCamp1_AspMVC.Data;
using BootCamp1_AspMVC.Dtos;
using BootCamp1_AspMVC.Repository.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BootCamp1_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;


        public ProductsController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }


        [HttpGet]
        public ActionResult<IEnumerable<ProductDto>> GetAll()
        {

            IEnumerable<ProductDto> products =
                _context.Products
                .AsNoTracking()
                .Include(e => e.Category)
                .Select(e => new ProductDto
                {
                    Id = e.Id,
                    ProductName = e.ProductName,
                    Price = e.Price,
                    Qty = e.Qty,
                    CategoryName = e.Category.Name,
                    Description = e.Description,
                    ImagePath = e.ImagePath
                })
                .AsQueryable();
            return Ok(products);

        }


        [HttpGet("{id:int}")]
        public ActionResult<ProductDto> GetById(int id) 
        {
            var product = _context.Products
            .AsNoTracking().
            Include(e => e.Category)
            .FirstOrDefault(e => e.Id == id);
            if (product == null)
            {
                return NotFound(new {Message ="لم يتم العثور علي المنتج."});
            }

            var productDto = new ProductDto
            {
                Id = product.Id,
                ProductName = product.ProductName,
                Price = product.Price,
                Qty = product.Qty,
                CategoryName = product.Category.Name,
                Description = product.Description,
                ImagePath = product.ImagePath
            };

            return Ok(productDto);


        }










    }
}
