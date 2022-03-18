using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Myfirst.Data;
using MyFirst.Models.Entities;
using MyFirst.Repository.DataContext;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BackendProject.Data
{
    public class DataInitializer
    {
        public readonly AppDbContext _dbContext;
        public readonly RoleManager<IdentityRole> _roleManager;
        public readonly UserManager<User> _userManager;

        public DataInitializer(AppDbContext dbContext, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task SeedDataAsync()
        {
            await _dbContext.Database.MigrateAsync();

            #region Default Rollarin json file-dan oxunmasi ve database-ne yazilmasi

            await MyCreateDefaultRolesFileAsync(); // json file yaradilmasi

            var roleJson = File.ReadAllText(@$"{Constants.SeedDataPath}\MyDefaultRoles.json");
            var jsonRoles = JsonConvert.DeserializeObject<List<string>>(roleJson);

            foreach (var role in jsonRoles)
            {
                if (await _roleManager.RoleExistsAsync(role))
                    continue;

                await _roleManager.CreateAsync(new IdentityRole(role));
            }
            #endregion

            #region Default Userlerin json file-dan oxunmasi ve database-ne yazilmasi

            await MyCreateDefaultUsersFileAsync(); // json file yaradilmasi

            var userJson = File.ReadAllText(@$"{Constants.SeedDataPath}\MyDefaultUsers.json");
            var jsonUsers = JsonConvert.DeserializeObject<List<User>>(userJson);

            foreach (var user in jsonUsers)
            {
                if (await _userManager.FindByNameAsync(user.UserName) != null)
                    continue;

                user.IsActive = true;
                await _userManager.CreateAsync(user, "Admin.123");
                await _userManager.AddToRoleAsync(user, RoleConstants.AdminRole);
            }
            #endregion
        }

        public async Task MyCreateDefaultRolesFileAsync()
        {
            var roles = new List<string>()
            {
                RoleConstants.AdminRole,
                RoleConstants.ModeratorRole,
                RoleConstants.UserRole
            };

            await MyFileUtil.MyCreateFileAsync<string>(roles, Constants.SeedDataPath, "MyDefaultRoles.json");
        }

        public async Task MyCreateDefaultUsersFileAsync()
        {
            var user = new User()
            {
                FullName = "Namik Heydarov",
                UserName = "Admin",
                Email = "namikah@code.edu.az",
                IsActive = true
            };

            var users = new List<User>();
            users.Add(user);

            await MyFileUtil.MyCreateFileAsync<User>(users, Constants.SeedDataPath, "MyDefaultUsers.json");
        }
    }
}
