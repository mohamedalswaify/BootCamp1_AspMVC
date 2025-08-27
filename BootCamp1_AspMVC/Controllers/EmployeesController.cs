using BootCamp1_AspMVC.Data;
using BootCamp1_AspMVC.Models;
using BootCamp1_AspMVC.Repository.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BootCamp1_AspMVC.Controllers
{
    public class EmployeesController : Controller
    {
        //private readonly ApplicationDbContext _context;

        //public EmployeesController(ApplicationDbContext context)
        //{
        //    _context = context;

        //}


        private readonly IRepository<Employee> _repository ;
        private readonly IRepository<Department> _repositoryDept ;

        public EmployeesController(IRepository<Employee> repository, IRepository<Department> repositoryDept)
        {

            _repository = repository;
            _repositoryDept = repositoryDept;


        }

        public IActionResult Index()
        {
    
            var emp = _repository.FindAll;
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
  
            _repository.Insert(emp);
            TempData["Success"] = "Employee created successfully!";
            return RedirectToAction("Index");

        }




        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var cate = _repository.FindById(Id);

            

            CreateListDepartment();
            return View(cate);
        }

        [HttpPost]
        public IActionResult Edit(Employee emp)
        {

            _repository.Update(emp);

            TempData["Update"] = "Employee updated successfully!";
            return RedirectToAction("Index");

        }



        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var cate = _repository.FindById(Id);

            return View(cate);
        }

        [HttpPost]
        public IActionResult Delete(Employee emp)
        {

            _repository.Delete(emp);

            TempData["Remove"] = "Employee deleted successfully!";
            return RedirectToAction("Index");

        }













    }
}
