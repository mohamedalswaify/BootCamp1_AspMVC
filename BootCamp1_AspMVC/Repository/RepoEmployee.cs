using BootCamp1_AspMVC.Data;
using BootCamp1_AspMVC.Models;
using BootCamp1_AspMVC.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace BootCamp1_AspMVC.Repository
{
    public class RepoEmployee : MainRepository<Employee>, IRepoEmployee
    {
        private readonly ApplicationDbContext _context;
        public RepoEmployee(ApplicationDbContext Context) :base(Context)
        {
            _context = Context;
        }

        public IEnumerable<Employee> FindAllemployee()
        {
            return _context.Employees.Include(e => e.Department).ToList();
        }

        public Employee LoginByUser(string userName, string password)
        {
           return _context.Employees.FirstOrDefault(e => e.Username == userName && e.Password == password);
        }



    }
}
