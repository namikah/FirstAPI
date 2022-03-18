using Microsoft.AspNetCore.Identity;
using MyFirst.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyFirst.Repository.Repository.Contracts
{
    public interface IUserRepository<T> where T : class
    {
        Task<IList<T>> GetAllAsync();
        Task<T> GetAsync(string id);
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
