using BootCamp1_AspMVC.Data;
using BootCamp1_AspMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
            var emp = _context.Employees.Include(e => e.Department).ToList();
            return View(emp);
        }


   

        private void CreateListDepartment(int selectId = 0)
        {
            List<Department> Dept = _context.Departments.ToList();
            SelectList DeptList = new SelectList(Dept, "Id", "Name", 0);
            ViewBag.DepartmentList = DeptList;
        }




        [HttpGet]
        public IActionResult Create()
        {
            CreateListDepartment();

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
            CreateListDepartment();
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
