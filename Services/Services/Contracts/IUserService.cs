using MyFirst.Models.DTOs;
using MyFirst.Models.Entities;
using MyFirst.Repository.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirst.Services.Services.Contracts
{
    public interface IUserService : IUserRepository<User>
    {
        object GetTest();

        Task<List<UserDto>> GetAllUsersAsync();
    }
}
