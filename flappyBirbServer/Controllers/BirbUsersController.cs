using flappyBirb_server.Models;
using flappyBirbServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace flappyBirbServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BirbUsersController : ControllerBase
    {
        readonly UserManager<BirbUser> _userManager;

        public BirbUsersController(UserManager<BirbUser> userManager)
        {
            this._userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (registerDTO.Password != registerDTO.ConfirmPassword)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new { Message = "Passwords do not match." });
            }

            var user = new BirbUser()
            {
                UserName = registerDTO.username,
                Email = registerDTO.Email
            };

            IdentityResult identityResult = await this._userManager.CreateAsync(user, registerDTO.Password);
            if (!identityResult.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { Message = "User creation failed." });
            }

            return Ok(new { Message = "User created successfully." });
        }

    }
}
