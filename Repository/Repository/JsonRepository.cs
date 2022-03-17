using Models.Base;
using Repository.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class JsonRepository<T> /*: IRepository<T> where T : class, IEntity*/
    {
        public Task AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(IEnumerable<T> entity)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(params T[] entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(IEnumerable<T> entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(params T[] entity)
        {
            throw new NotImplementedException();
        }

        public Task<IList<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(IEnumerable<T> entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(params T[] entity)
        {
            throw new NotImplementedException();
        }
    }
}
