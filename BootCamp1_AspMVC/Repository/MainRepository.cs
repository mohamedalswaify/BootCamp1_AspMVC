using BootCamp1_AspMVC.Data;
using BootCamp1_AspMVC.Repository.Base;

namespace BootCamp1_AspMVC.Repository
{
    public class MainRepository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        public MainRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public IEnumerable<T> FindAll()
        {
            return _context.Set<T>().ToList();
        }

        public T FindById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Insert(T myItem)
        {
            _context.Set<T>().Add(myItem);
            _context.SaveChanges();
        }

        public void Update(T myItem)
        {
            _context.Set<T>().Update(myItem);
            _context.SaveChanges();
        }

        public void Delete(T myItem)
        {
           _context.Set<T>().Remove(myItem);
            _context.SaveChanges();
        }

    }
}
