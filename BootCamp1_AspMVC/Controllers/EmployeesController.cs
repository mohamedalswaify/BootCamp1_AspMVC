using BootCamp1_AspMVC.Data;
using BootCamp1_AspMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BootCamp1_AspMVC.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;

        }

        public IActionResult Index()
        {
            var emp = _context.Employees.ToList();
            return View(emp);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee emp)
        {
            _context.Employees.Add(emp);
            _context.SaveChanges();
            TempData["Success"] = "Employee created successfully!";
            return RedirectToAction("Index");

        }




        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var cate = _context.Employees.Find(Id);
            return View(cate);
        }

        [HttpPost]
        public IActionResult Edit(Employee emp)
        {
            _context.Employees.Update(emp);
            _context.SaveChanges();
            TempData["Update"] = "Employee updated successfully!";
            return RedirectToAction("Index");

        }



        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var cate = _context.Employees.Find(Id);
            return View(cate);
        }

        [HttpPost]
        public IActionResult Delete(Employee emp)
        {
            _context.Employees.Remove(emp);
            _context.SaveChanges();
            TempData["Remove"] = "Employee deleted successfully!";
            return RedirectToAction("Index");

        }













    }
}
