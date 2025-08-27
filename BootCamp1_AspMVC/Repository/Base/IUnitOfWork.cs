using BootCamp1_AspMVC.Models;

namespace BootCamp1_AspMVC.Repository.Base
{
    public interface IUnitOfWork
    {
        IRepoEmployee Employees { get; }
        IRepository<Department> Departments { get; }
        IRepository<Category> Categories { get; }
   
    }
}
