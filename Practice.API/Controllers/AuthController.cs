using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Practice.API.Models.DTO;

namespace Practice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;

        public AuthController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        //Create User
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username,
            };

            var identityResults = await userManager.CreateAsync(identityUser , registerRequestDto.Password);
            
            if(identityResults.Succeeded)
            {
                if(registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    identityResults = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);

                    if(identityResults.Succeeded)
                    {
                        return Ok("User Registered Successfully : Please Login");
                    }
                }
            }
            return BadRequest(identityResults);
        }
    }
}
