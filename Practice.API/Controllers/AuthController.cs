using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Practice.API.Models.DTO;
using Practice.API.Repository;

namespace Practice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager , ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
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

        // Here we used login to validate user and create token
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            var user = await userManager.FindByEmailAsync(loginRequest.Username);
            if(user != null)
            {
                var checkPassword = await userManager.CheckPasswordAsync(user, loginRequest.Password);
                if(checkPassword)
                {
                    var userRoles = await userManager.GetRolesAsync(user);
                    if(userRoles !=null)
                    {
                        var jwtToken = tokenRepository.CreateToken(user, userRoles.ToList());
                        var response = new LoginResponseDto()
                        {
                            JwtToken = jwtToken
                        };
                        return Ok(response);
                    }
                }
            }
            return BadRequest("Email or Password is incorrect");
        }
    }
}
