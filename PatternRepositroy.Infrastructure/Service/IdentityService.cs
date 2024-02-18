using Azure.Messaging;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using PatternRepository.Application.Helper;
using PatternRepository.Application.IdentityModels;
using PatternRepository.Application.Interface.Service;
using PatternRepositroy.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PatternRepositroy.Infrastructure.Service
{
    public class IdentityService : IIdentityService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;

        public IdentityService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<JWT> jwt)
        {

            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = jwt.Value;
        }



        public async Task<AuthModel> RegisterAsync(RegisterModel model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
            {
                return new AuthModel { Message = "Email is already Registered" };
            }
            if (await _userManager.FindByNameAsync(model.UserName) is not null)
            {
                return new AuthModel { Message = "UserName is already Registered" };
            }
            var user = new ApplicationUser()
            {
                FisrtName = model.FirstName,
                LAstName = model.LastName,
                Email = model.Email,
                UserName = model.UserName,
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description},";
                }
                return new AuthModel { Message = errors };
            }
            await _userManager.AddToRoleAsync(user, "User");

            var jwtSecurityToken = await CreateJwtToken(user);
            return new AuthModel
            {
                Email = user.Email,
                IsAuthenticated = true,
                ExpiresOn = jwtSecurityToken.ValidTo,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                UserName = user.UserName,
            };


        }
        public async Task<AuthModel> LoginAsync(LoginModel model)
        {
            var authModel = new AuthModel();
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null || !await _userManager.CheckPasswordAsync(user, model.PassWord))
            {
                authModel.Message = "Email Or PassWord Incorrect!";
                return authModel;
            }
            var jwtSecurityToken = await CreateJwtToken(user);

            var roleList = await _userManager.GetRolesAsync(user);

            authModel.IsAuthenticated = true;
            authModel.Email = user.Email!;
            authModel.UserName = user.UserName!;
            authModel.ExpiresOn = jwtSecurityToken.ValidTo;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Roles = roleList.ToList();
            return authModel;

        }
        public async Task<string> AddRoleAsync(AddRoleModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user is null || !await _roleManager.RoleExistsAsync(model.Role))
                return "Invalid userId Or Role";
            if (await _userManager.IsInRoleAsync(user, model.Role))
                return "User already assigned to this role";
            var result = await _userManager.AddToRoleAsync(user, model.Role);
            return result.Succeeded ? string.Empty : "Something went Wrong";
        }
        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        public async Task<AuthModel> RemoveRoleAsync(IdentityRole role)
        {
            var x = await _roleManager.DeleteAsync(role);
            return new AuthModel()
            {
                Message = $"{x} + Roles Deleted Successfully "
            };

        }

        public async Task<string> CreateRoleAsync(string rolename)
        {
            var result = await _roleManager.CreateAsync(new IdentityRole { Name = rolename });

            return result.Succeeded ? string.Empty : "Something went Wrong";

        }
    }
}
