using AutoMapper;
using BackendProject.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Myfirst.Data;
using MyFirst.AuthenticationService;
using MyFirst.AuthenticationService.Contracts;
using MyFirst.AuthenticationService.Models;
using MyFirst.Models.DTOs;
using MyFirst.Models.Entities;
using MyFirst.Services.Services.Contracts;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace MyFirst.Services.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IOptions<JwtSetting> jwt, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<string> RegisterAsync(UserDto model)
        {
            var user = _mapper.Map<User>(model);

            var userWithSameEmail = await _userManager.FindByEmailAsync(model.Email);
            if (userWithSameEmail == null)
            {
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, RoleConstants.UserRole.ToString());
                }
                return $"User Registered with username {user.UserName}";
            }
            else
            {
                return $"Email {user.Email } is already registered.";
            }
        }

        public async Task<List<User>> GetAsync()
        {
            return await _userManager.Users.ToListAsync();
        }
    }
}
