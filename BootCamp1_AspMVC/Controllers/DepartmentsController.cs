using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BootCamp1_AspMVC.Data;
using BootCamp1_AspMVC.Models;
using BootCamp1_AspMVC.Repository.Base;

namespace BootCamp1_AspMVC.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IRepository<Department> _repository;

        public DepartmentsController(IRepository<Department> repository)
        {
            _repository = repository;

        }



        public IActionResult Index()
        {
            var dept = _repository.FindAll();
            return View(dept);
        }


        // GET: Departments/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department =  _repository.FindById(id.Value);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Department department)
        {
            if (ModelState.IsValid)
            {
                _repository.Insert(department);
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = _repository.FindById(id.Value);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Department department)
        {
            if (id != department.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
            _repository.Update(department);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = _repository.FindById(id.Value);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department =_repository.FindById(id);
            if (department != null)
            {
                _repository.Delete(department);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentExists(int id)
        {
                var cate = _repository.FindById(id);   
                if (cate == null)
                {
                    return false;
                }
                return true;
               
        }
    }
}
