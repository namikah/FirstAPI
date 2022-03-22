using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyFirst.AuthenticationService.Contracts;
using MyFirst.AuthenticationService.Models;
using MyFirst.Models.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyFirst.AuthenticationService
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtSetting _jwtSetting;

        public AuthService(UserManager<User> userManager, IOptions<JwtSetting> jwtSetting)
        {
            _userManager = userManager;
            _jwtSetting = jwtSetting.Value;
        }

        public async Task<AuthenticationModel> GetTokenAsync(CredentialModel model)
        {
            var authenticationModel = new AuthenticationModel();
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = $"No Accounts Registered with {model.Username}.";
                return authenticationModel;
            }

            if (await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authenticationModel.IsAuthenticated = true;
                var jwtSecurityToken = await CreateJwtToken(user);
                authenticationModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                authenticationModel.Email = user.Email;
                authenticationModel.UserName = user.UserName;
                var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                authenticationModel.Roles = rolesList.ToList();
                return authenticationModel;
            }
            authenticationModel.IsAuthenticated = false;
            authenticationModel.Message = $"Incorrect Credentials for user {user.UserName}.";
            return authenticationModel;
        }

        private async Task<JwtSecurityToken> CreateJwtToken(User user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
            }
            .Union(userClaims)
            .Union(roleClaims);
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSetting.Issuer,
                audience: _jwtSetting.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSetting.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }

        //    private readonly JwtSetting _jwtSetting;
        //    private readonly IUserService _userService;
        //    //private readonly List<User> _users;
        //    //private readonly UserManager _userManager;

        //    public AuthService(IOptions<JwtSetting> jwtSetting, IUserService userService)
        //    {
        //        _jwtSetting = jwtSetting.Value;
        //        _userService = userService;
        //        //_users = new List<UserDto>
        //        //{
        //        //    new User {Username = "Aydin", Password = "123", Fullname = "Aydin Yusubov"},
        //        //    new User {Username = "Aydin2", Password = "123", Fullname = "Aydin2 Yusubov2"},
        //        //    new User {Username = "Aydin3", Password = "123", Fullname = "Aydin3 Yusubov3"},
        //        //};
        //    }

        //    public async Task<string> GetToken(CredentialModel credentialModel)
        //    {
        //        var user = await _userService.FindAsync(credentialModel.Username);
        //        if (user == null)
        //        {
        //            throw new Exception("Invalid Credentials");
        //        }

        //        //if (user.PasswordHash != credentialModel.Password)
        //        //{
        //        //    throw new Exception("Invalid Credentials");
        //        //}

        //        var jwtSecurityToken = CreateJwtToken(user);
        //        var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        //        return token;
        //    }

        //    private JwtSecurityToken CreateJwtToken(User user)
        //    {
        //        var claims = new List<Claim>
        //        {
        //            new Claim("roles", "Admin"),
        //            new Claim("roles", "User"),
        //            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
        //        };

        //        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.Key));
        //        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        //        var jwtSecurityToken = new JwtSecurityToken(
        //            _jwtSetting.Issuer,
        //            _jwtSetting.Audience,
        //            claims,
        //            expires: DateTime.UtcNow.AddMinutes(_jwtSetting.DurationInMinutes),
        //            signingCredentials: signingCredentials);
        //        return jwtSecurityToken;
        //    }
    }
}
