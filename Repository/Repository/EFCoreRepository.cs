using Microsoft.EntityFrameworkCore;
using MyFirst.Models.Base;
using MyFirst.Repository.DataContext;
using MyFirst.Repository.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirst.Repository.Repository
{
    public class EFCoreRepository<T> : IRepository<T> where T : class, IEntity
    {
        protected readonly AppDbContext DbContext;

        public EFCoreRepository(AppDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await DbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            return await DbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(T entity)
        {
            await DbContext.Set<T>().AddAsync(entity);
            await DbContext.SaveChangesAsync();
        }

        public Task AddAsync(IEnumerable<T> entity)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(params T[] entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(T entity)
        {
            DbContext.Set<T>().Remove(entity);
            await DbContext.SaveChangesAsync();
        }

        public Task DeleteAsync(IEnumerable<T> entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(params T[] entity)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(T entity)
        {
            DbContext.Set<T>().Update(entity);
            await DbContext.SaveChangesAsync();
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
