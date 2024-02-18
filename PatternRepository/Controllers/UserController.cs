using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatternRepository.Application.IdentityModels;
using PatternRepository.Application.Interface.Service;
using PatternRepositroy.Infrastructure.Service;

namespace PatternRepository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService<ApplicationUser> _userService;

        public UserController(IUserService<ApplicationUser> userService)
        {
            _userService = userService;
        }

        [HttpGet("Users")]
        public async Task<IActionResult> GetUsers()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var users = await _userService.GetListAsync();
            if (users == null)
                return NotFound();
            return Ok(users);   
        }

        [HttpGet]
        public async Task<IActionResult> GetUser(string id)
        {
            if(id== null)
                return NotFound();

            var user= await _userService.GetAsync(id);
            if(user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userService.GetAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            await _userService.DeleteAsync(user);
            return Ok(user);
        }

    }
}
