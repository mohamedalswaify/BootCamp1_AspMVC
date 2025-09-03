using BootCamp1_AspMVC.Data;
using BootCamp1_AspMVC.Dtos;
using BootCamp1_AspMVC.Models;
using BootCamp1_AspMVC.Repository.Base;
using Microsoft.AspNetCore.Mvc;

namespace BootCamp1_AspMVC.Controllers
{
    public class CategoriesController : Controller
    {
        //private readonly ApplicationDbContext _context;

        //public CategoriesController(ApplicationDbContext context)
        //{
        //    _context = context;

        //}



        //private readonly IRepository<Category> _repository;

        //public CategoriesController(IRepository<Category> repository)
        //{
        //    _repository = repository;


        //}



        private readonly IUnitOfWork _unitOfWork;
        public CategoriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }




        public IActionResult Index()
        {
           //var categories = _context.Categories.ToList();
           var categories = _unitOfWork.Categories.FindAll();

          
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

            if(!ModelState.IsValid)
            {
                return View(category);
            }
            //_context.Categories.Add(category);
            //_context.SaveChanges();

            _unitOfWork.Categories.Insert(category);

            TempData["Success"] = "Category created successfully!";
            return RedirectToAction("Index");
           
        }




        [HttpGet]
        public IActionResult Edit(int Id)
        {
            //var cate= _context.Categories.Find(Id);
            var cate = _unitOfWork.Categories.FindById(Id);
            return View(cate);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            //_context.Categories.Update(category);
            //_context.SaveChanges();

            _unitOfWork.Categories.Update(category);

            TempData["Update"] = "Category updated successfully!";
            return RedirectToAction("Index");

        }



        [HttpGet]
        public IActionResult Delete(int Id)
        {
            //var cate = _context.Categories.Find(Id);
            var cate = _unitOfWork.Categories.FindById(Id);
            return View(cate);
        }

        [HttpPost]
        public IActionResult Delete(Category category)
        {
            //_context.Categories.Remove(category);
            //_context.SaveChanges();
            _unitOfWork.Categories.Delete(category);
            TempData["Remove"] = "Category deleted successfully!";
            return RedirectToAction("Index");

        }






    }

}
