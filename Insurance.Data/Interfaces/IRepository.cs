using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance.Data.Interfaces
{
    /// <summary>
    /// Interface that defines repository.
    /// This is an abstraction over different ORM implementations (EF Core, Dapper etc)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T: class
    {
        Task<List<T>> GetAll();
        Task<T> Get(int id);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(int id);
    }
}
