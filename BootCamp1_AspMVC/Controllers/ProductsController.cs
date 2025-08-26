using BootCamp1_AspMVC.Data;
using BootCamp1_AspMVC.Dtos;
using BootCamp1_AspMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BootCamp1_AspMVC.Controllers
{
    public class ProductsController : Controller
    {

        private readonly ApplicationDbContext _context;


        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            List<ProductDto> products = 
                _context.Products
                .Include(e=>e.Category)
                .Select(e=> new ProductDto { 
                Id = e.Id,
                    ProductName=e.ProductName,
                    Price = e.Price,
                    Qty = e.Qty,
                    CategoryName = e.Category.Name,
                    Description = e.Description
                })
                .ToList();
            return View(products);

        }

        public IActionResult GetAll()
        {

            List<ProductDto> products =
                _context.Products
                .Include(e => e.Category)
                .Select(e => new ProductDto
                {
                    Id = e.Id,
                    ProductName = e.ProductName,
                    Price = e.Price,
                    Qty = e.Qty,
                    CategoryName = e.Category.Name,
                    Description = e.Description
                })
                .ToList();
            return Ok(products);

        }




        private  void CreateListCategory(int selectId =0)
        {
            //List<CategoryDto> categories = new List<CategoryDto>()
            //{
            //    new CategoryDto{Id = 0,Name = "SelectCategory"},
            //    new CategoryDto{Id = 1,Name = "Electronics"},
            //    new CategoryDto {Id = 2,Name = "Clothing",},
            //    new CategoryDto{Id = 3,Name = "Books",},
            //    new CategoryDto{Id = 4,Name = "Home & Kitchen",},
            //};


            List<Category> categories =  _context.Categories.ToList();
            SelectList cateList = new SelectList(categories, "Id", "Name", 0);
            ViewBag.CategoryList = cateList;
        }




        [HttpGet]
        public IActionResult Create()
        {
            CreateListCategory();


            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            TempData["Success"] = "Product created successfully!";
            return RedirectToAction("Index");

        }


        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var cate = _context.Products.Find(Id);
            CreateListCategory();
            return View(cate);
        }

        [HttpPost]
        public IActionResult Edit(Product pro)
        {
            _context.Products.Update(pro);
            _context.SaveChanges();
            TempData["Update"] = "Product updated successfully!";
            return RedirectToAction("Index");

        }



        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var cate = _context.Products.Find(Id);
            return View(cate);
        }

        [HttpPost]
        public IActionResult Delete(Product pro)
        {
            _context.Products.Remove(pro);
            _context.SaveChanges();
            TempData["Remove"] = "Product deleted successfully!";
            return RedirectToAction("Index");

        }










    }
}
