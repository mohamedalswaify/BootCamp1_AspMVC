namespace BootCamp1_AspMVC.Repository.Base
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> FindAll();
        T FindById(int id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T myItem);
       
    }
}
