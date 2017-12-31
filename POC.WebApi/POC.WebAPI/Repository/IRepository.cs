using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace POC.WebAPI.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> Get();
        Task<T> Get(string id);
        Task<bool> Get(T entity);
        Task Add(T entity);
        Task<T> Update(string id, T entity);
        Task<DeleteResult> Remove(string id);
    }
}
