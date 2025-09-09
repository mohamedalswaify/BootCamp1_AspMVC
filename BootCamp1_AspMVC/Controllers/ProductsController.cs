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
        private readonly IWebHostEnvironment _env;


        public ProductsController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
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
                    Description = e.Description,
                    ImagePath = e.ImagePath
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
                    Description = e.Description,
                    ImagePath = e.ImagePath
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



        private string? SaveImage(IFormFile? file)
        {
            if (file == null || file.Length == 0) return null;

            // التحقق من الامتداد (اختياري لكنه مهم)
            var allowed = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowed.Contains(ext))
                throw new InvalidOperationException("امتداد الملف غير مسموح");

            // مسار المجلد داخل wwwroot
            var folder = Path.Combine("uploads", "products");
            var rootFolder = Path.Combine(_env.WebRootPath, folder);

            // إنشاء المجلد لو غير موجود
            Directory.CreateDirectory(rootFolder);

            // اسم ملف فريد
            var fileName = $"{Guid.NewGuid():N}{ext}";
            var fullPath = Path.Combine(rootFolder, fileName);

            using (var stream = System.IO.File.Create(fullPath))
            {
                file.CopyTo(stream);
            }

            // نعيد المسار النسبي للاستخدام في <img src="~/{path}">
            var relativePath = Path.Combine(folder, fileName).Replace('\\', '/');
            return "/" + relativePath;
        }



        private void DeleteImageIfExists(string? relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath)) return;

            var fullPath = Path.Combine(_env.WebRootPath, relativePath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
        }




        [HttpGet]
        public IActionResult Create()
        {
            CreateListCategory();


            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product, IFormFile ImageFile)
        {
            var path = SaveImage(ImageFile);
            product.ImagePath = path;
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
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product pro, IFormFile? ImageFile)
        {
            try
            {
                var existing = _context.Products.AsNoTracking().FirstOrDefault(x => x.Id == pro.Id);
                if (existing == null) return NotFound();

                // لو في صورة جديدة: احذف القديمة واحفظ الجديدة
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    // احذف القديمة (إن وجدت)
                    DeleteImageIfExists(existing.ImagePath);

                    // احفظ الجديدة
                    pro.ImagePath = SaveImage(ImageFile);
                }
                else
                {
                    // احتفظ بالقديمة كما هي
                    pro.ImagePath = existing.ImagePath;
                }

                _context.Products.Update(pro);
                _context.SaveChanges();
                TempData["Update"] = "Product updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                CreateListCategory(pro.CategoryId);
                return View(pro);
            }
        }




        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var cate = _context.Products.Find(Id);
            return View(cate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Product pro)
        {
            var existing = _context.Products.Find(pro.Id);
            if (existing == null) return NotFound();

            // احذف الصورة من السيرفر أولاً
            DeleteImageIfExists(existing.ImagePath);

            _context.Products.Remove(existing);
            _context.SaveChanges();
            TempData["Remove"] = "Product deleted successfully!";
            return RedirectToAction(nameof(Index));
        }









    }
}
