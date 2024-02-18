using Microsoft.AspNetCore.Identity;
using PatternRepository.Application.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternRepository.Application.Interface.Service
{
    public interface IIdentityService
    {
        Task<AuthModel> RegisterAsync(RegisterModel model);
        Task<AuthModel> LoginAsync(LoginModel model);
        Task<string> AddRoleAsync(AddRoleModel model);
        Task<AuthModel> RemoveRoleAsync(IdentityRole role);
        Task<string> CreateRoleAsync(string rolename);

        
        
       
    }
}
