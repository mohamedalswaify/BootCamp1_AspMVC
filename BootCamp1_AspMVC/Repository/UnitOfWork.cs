using BootCamp1_AspMVC.Data;
using BootCamp1_AspMVC.Models;
using BootCamp1_AspMVC.Repository.Base;
using System;

namespace BootCamp1_AspMVC.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            Employees = new RepoEmployee(_context);
            Departments = new MainRepository<Department>(_context);
            Categories = new MainRepository<Category>(_context);

        }


        private readonly ApplicationDbContext _context;

        public   IRepoEmployee Employees { get; }
        public  IRepository<Department> Departments { get; }

        public IRepository<Category> Categories { get; }
    }
}
