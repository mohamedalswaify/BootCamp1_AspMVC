using BootCamp1_AspMVC.Data;
using BootCamp1_AspMVC.Models;
using BootCamp1_AspMVC.Repository.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace BootCamp1_AspMVC.Controllers
{
    public class EmployeesController : Controller
    {
        //private readonly ApplicationDbContext _context;

        //public EmployeesController(ApplicationDbContext context)
        //{
        //    _context = context;

        //}


        //private readonly IRepository<Employee> _repository ;
        private readonly IRepoEmployee _repEmployee;
        private readonly IRepository<Department> _repositoryDept ;

        public EmployeesController (IRepoEmployee repEmployee, IRepository<Department> repositoryDept)
        {

            _repEmployee = repEmployee;
            _repositoryDept = repositoryDept;


        }

        public IActionResult Index()
        {
    
            var emp = _repEmployee.FindAllemployee();
            return View(emp);
        }


   

        private void CreateListDepartment(int selectId = 0)
        {
            IEnumerable<Department> Dept = _repositoryDept.FindAll();
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

            _repEmployee.Insert(emp);
            TempData["Success"] = "Employee created successfully!";
            return RedirectToAction("Index");

        }




        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var cate = _repEmployee.FindById(Id);

            

            CreateListDepartment();
            return View(cate);
        }

        [HttpPost]
        public IActionResult Edit(Employee emp)
        {

            _repEmployee.Update(emp);

            TempData["Update"] = "Employee updated successfully!";
            return RedirectToAction("Index");

        }



        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var cate = _repEmployee.FindById(Id);

            return View(cate);
        }

        [HttpPost]
        public IActionResult Delete(Employee emp)
        {

            _repEmployee.Delete(emp);

            TempData["Remove"] = "Employee deleted successfully!";
            return RedirectToAction("Index");

        }













    }
}
