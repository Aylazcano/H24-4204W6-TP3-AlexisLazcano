using flappyBirb_server.Models;
using flappyBirbServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace flappyBirbServer.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BirbUsersController : ControllerBase
    {
        readonly UserManager<BirbUser> _userManager;
        IConfiguration _configuration;

        public BirbUsersController(UserManager<BirbUser> userManager, IConfiguration configuration)
        {
            this._userManager = userManager;
            this._configuration = configuration;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (registerDTO.Password != registerDTO.ConfirmPassword)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new { Message = "Passwords do not match." });
            }

            var user = new BirbUser()
            {
                UserName = registerDTO.Username,
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

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            BirbUser user = await this._userManager.FindByNameAsync(loginDTO.Username);
            if (user != null && await this._userManager.CheckPasswordAsync(user, loginDTO.Password))
            {
                IList<string> roles = await this._userManager.GetRolesAsync(user);
                List<Claim> authClaims = new List<Claim>();
                foreach (string role in roles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }
                authClaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8
                    .GetBytes(this._configuration["JWT:Secret"]));
                JwtSecurityToken token = new JwtSecurityToken(
                    issuer: "https://localhost:7065",
                    audience: "http://localhost:4020",
                    claims: authClaims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
                );
                return Ok(new { 
                    Message = "Login successful.",
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    validTo = token.ValidTo
                });;
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                                       new { Message = "Invalid login attempt." });
            }
        }

    }

}
