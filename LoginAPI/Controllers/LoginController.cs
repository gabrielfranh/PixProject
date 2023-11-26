using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace LoginAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //[HttpPost]
        //public IActionResult Login([FromBody] LoginDTO loginDTO)
        //{
        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Jwt:Key")));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //    var Sectoken = new JwtSecurityToken(expires: DateTime.Now.AddMinutes(120), signingCredentials: credentials);
        //    var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

        //    return Ok(token);
        //}
    }
}
