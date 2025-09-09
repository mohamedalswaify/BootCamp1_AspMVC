using BootCamp1_Api.Dtos;
using BootCamp1_AspMVC.Data;
using BootCamp1_AspMVC.Dtos;
using BootCamp1_AspMVC.Models;
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
                return NotFound(new { Message = "لم يتم العثور علي المنتج." });
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








        [HttpPost]
        [RequestSizeLimit(20 * 1024 * 1024)] // 20 ميجابايت
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest productDto)
        {
            var categoryExists = await _context.Categories
                .AnyAsync(c => c.Id == productDto.CategoryId);
            if (!categoryExists)
                return BadRequest(new { Message = "الفئه المحدد غير موجودة." });

            var product = new Product
            {
                ProductName = productDto.ProductName,
                Price = productDto.Price,
                Qty = productDto.Qty,
                CategoryId = productDto.CategoryId,
                Description = productDto.Description,
                //  ImagePath = SaveImage(productDto.Image)
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);


        }




        [HttpPut("{id:int}")]
        [RequestSizeLimit(20 * 1024 * 1024)] // 20 ميجابايت
        public async Task<IActionResult> update(int id,[FromForm] ProductUpdateRequest productDto)
        {
            if (id != productDto.Id)
                return BadRequest(new { Message = "المعرف المرسل لا يطابق مسار الطلب." });

            var categoryExists = await _context.Categories
                .AnyAsync(c => c.Id == productDto.CategoryId);
            if (!categoryExists)
                return BadRequest(new { Message = "الفئه المحدد غير موجودة." });

            var existing = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (existing == null) return NotFound(new { Message = "لم يتم العثور على المنتج." });

            existing.ProductName =productDto.ProductName;
            existing.Price = productDto.Price;
            existing.Qty = productDto.Qty;
            existing.CategoryId = productDto.CategoryId;
            existing.Description = productDto.Description;


            _context.Products.Update(existing); 
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = productDto.Id }, existing);


        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (existing == null) return NotFound(new { Message = "لم يتم العثور على المنتج." });

            _context.Products.Remove(existing);
            await _context.SaveChangesAsync();
            return Ok(new {Message="تم الحذف بنجاح"});
        }








    }
}
