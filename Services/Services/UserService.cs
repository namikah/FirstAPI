using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MyFirst.Models.DTOs;
using MyFirst.Models.Entities;
using MyFirst.Repository.DataContext;
using MyFirst.Repository.Repository;
using MyFirst.Services.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirst.Services.Services
{
    public class UserService : UserRepository<User>, IUserService
    {
        private readonly IMapper _mapper;

        public UserService(IMapper mapper, UserManager<User> userManager) : base(userManager)
        {
            _mapper = mapper;
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var users = await GetAllAsync();

            return _mapper.Map<List<UserDto>>(users);
        }

        public object GetTest()
        {
            return "Test";
        }
    }
}
