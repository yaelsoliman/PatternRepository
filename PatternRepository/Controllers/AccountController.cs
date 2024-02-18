using JobFinder.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PatternRepository.Application.IdentityModels;
using PatternRepository.Application.Interface.Service;
using PatternRepository.Application.Shared;

namespace PatternRepository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly ICurrentUser _currentUser;
        private readonly IEmailService _emailService;

        public AccountController(IIdentityService identityService,ICurrentUser current,IEmailService emailService)
        {
            _identityService = identityService;
            _currentUser = current;
            _emailService = emailService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)  
                 return BadRequest(ModelState);
            
            var result=await _identityService.RegisterAsync(model);
            if(!result.IsAuthenticated)
                return BadRequest(result.Message);
            return Ok(result);

        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _identityService.LoginAsync(model);
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);
            return Ok(result);

        }
        //[Authorize("Admin")]
        [HttpPost("AddRole")]
        public async Task<IActionResult> AddToRoleAsync([FromBody] AddRoleModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _identityService.AddRoleAsync(model);
            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);
            return Ok(model);

        }
        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmail()
        {
            
                Mailrequest mailrequest = new Mailrequest();
                mailrequest.ToEmail = "nezarsoliman505@gmail.com";
                mailrequest.Subject = "Welcome to ASP.NET";
                mailrequest.Body = "Thanks for subscribing us.";
                await _emailService.SendEmailAsync(mailrequest);
                return Ok(mailrequest);
            
            
        }
        [HttpDelete("role")]
        public async Task<IActionResult> DeleteRole(IdentityRole role)
        {
            var result = await _identityService.RemoveRoleAsync(role);
            return Ok(result);
        }

        [HttpPost("Create Role")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            var result = await _identityService.CreateRoleAsync(roleName);
            return Ok(result);
        }

    }
}
