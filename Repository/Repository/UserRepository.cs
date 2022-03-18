using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyFirst.Models.Base;
using MyFirst.Models.Entities;
using MyFirst.Repository.DataContext;
using MyFirst.Repository.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirst.Repository.Repository
{
    public class UserRepository<T> : IUserRepository<User>
    {
        private readonly UserManager<User> _userManager;

        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IList<User>> GetAllAsync()
        {
            return await _userManager.Users.AsNoTracking().ToListAsync();
        }

        public async Task<User> GetAsync(string id)
        {
            return await _userManager.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(User entity)
        {
            await _userManager.CreateAsync(entity);
        }


        public async Task AddAsync(IEnumerable<User> entities)
        {
            foreach (var entity in entities)
            {
                await _userManager.CreateAsync(entity);
            }
        }

        public async Task AddAsync(params User[] entities)
        {
            foreach (var entity in entities)
            {
                await _userManager.CreateAsync(entity);
            }
        }

        public async Task DeleteAsync(User entity)
        {
            await _userManager.DeleteAsync(entity);
        }

        public async Task DeleteAsync(IEnumerable<User> entities)
        {
            foreach (var entity in entities)
            {
                await _userManager.DeleteAsync(entity);
            }
        }

        public async Task DeleteAsync(params User[] entities)
        {
            foreach (var entity in entities)
            {
                await _userManager.DeleteAsync(entity);
            }
        }

        public async Task UpdateAsync(User entity)
        {
            await _userManager.UpdateAsync(entity);
        }

        public async Task UpdateAsync(IEnumerable<User> entities)
        {
            foreach (var entity in entities)
            {
                await _userManager.UpdateAsync(entity);
            }
        }

        public async Task UpdateAsync(params User[] entities)
        {
            foreach (var entity in entities)
            {
                await _userManager.UpdateAsync(entity);
            }
        }

       
    }
}
