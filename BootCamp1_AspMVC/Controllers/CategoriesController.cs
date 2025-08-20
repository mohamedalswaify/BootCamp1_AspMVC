using BootCamp1_AspMVC.Data;
using BootCamp1_AspMVC.Dtos;
using BootCamp1_AspMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BootCamp1_AspMVC.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;

        }

        public IActionResult Index()
        {
           var categories = _context.Categories.ToList();

          
            return View(categories);
        }


        [HttpGet]
        public IActionResult Create()
        {

       

            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            TempData["Success"] = "Category created successfully!";
            return RedirectToAction("Index");
           
        }




        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var cate= _context.Categories.Find(Id);
            return View(cate);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
            TempData["Update"] = "Category updated successfully!";
            return RedirectToAction("Index");

        }



        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var cate = _context.Categories.Find(Id);
            return View(cate);
        }

        [HttpPost]
        public IActionResult Delete(Category category)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
            TempData["Remove"] = "Category deleted successfully!";
            return RedirectToAction("Index");

        }






    }

}
