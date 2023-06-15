using ApiCatalogo.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ApiCatalogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        public UserManager<IdentityUser> _userManager { get; set; }
        public SignInManager<IdentityUser> _signInManager { get; set; }
        public IConfiguration _config { get; set; }

        public AuthorizationController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration config) 
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            return "AutorizationController :: Acessado em:" + DateTime.Now.ToLongDateString();
        }

        //método para registro de cliente
        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody] UserDTO model)
        {
            var user = new IdentityUser
            {
                UserName = model.Email, Email = model.Email, EmailConfirmed = true 
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded) 
            {
                return BadRequest(result.Errors);
            }
            await _signInManager.SignInAsync(user, false);
            return Ok(CreateToken(model));
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserDTO user)
        {
            var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, isPersistent: false, lockoutOnFailure : false);

            if(result.Succeeded)
            {
                return Ok(CreateToken(user));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Login invalidado");
                return BadRequest();
            }
        }

        private UserToken CreateToken(UserDTO userInfo)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),
                new Claim("meuPet", "Pipoca"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            //gera a chave
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            //gera a assinatura digital

            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddHours(double.Parse(_config["TokenConfiguration:ExpireHours"]));

            var token = new JwtSecurityToken(issuer: _config["TokenConfiguration:Issuer"], audience: _config["TokenConfiguration:Audience"], claims: claims, expires: expiration, signingCredentials: credential);

            return new UserToken()
            {
                Authenticated = true,
                Expiration = expiration,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Message = "Token created"
            };
        }
    }
}
