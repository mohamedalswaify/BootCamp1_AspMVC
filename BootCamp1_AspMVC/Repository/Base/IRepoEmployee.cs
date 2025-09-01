using BootCamp1_AspMVC.Models;

namespace BootCamp1_AspMVC.Repository.Base
{
    public interface IRepoEmployee :IRepository<Employee> 
    {

        IEnumerable<Employee> FindAllemployee();


        Employee LoginByUser(string userName,string password);
    }
}
