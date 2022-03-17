using Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Contracts
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<IList<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        Task AddAsync(T entity);
        Task AddAsync(IEnumerable<T> entity);
        Task AddAsync(params T[] entity);
        Task UpdateAsync(T entity);
        Task UpdateAsync(IEnumerable<T> entity);
        Task UpdateAsync(params T[] entity);
        Task DeleteAsync(T entity);
        Task DeleteAsync(IEnumerable<T> entity);
        Task DeleteAsync(params T[] entity);
    }
}
