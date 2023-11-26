using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.RegularExpressions;
using UserAPI.DTOs;
using UserAPI.Services.Interfaces;

namespace UserAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] int id)
        {
            var user = await _userService.GetUserById(id);

            if (user is null)
                return NotFound(id);

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO userDTO)
        {
            var errorMessage = ValidateUserCreation(userDTO);

            if (!string.IsNullOrEmpty(errorMessage))
                return BadRequest(errorMessage);

            await _userService.CreateUser(userDTO);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserDTO userDTO)
        {
            if (userDTO is null)
                return BadRequest(userDTO);

            var success = await _userService.UpdateUser(userDTO);

            if (success)
                return Ok(userDTO);

            return NotFound(userDTO);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUserById([FromRoute] int id)
        {
            var success = await _userService.DeleteUserById(id);
            if (success)
                return Ok(success);

            return NotFound(id);
        }

        private string ValidateUserCreation(UserDTO userDTO)
        {
            if (string.IsNullOrEmpty(userDTO.Username))
                return "Username não pode ser vazio";
            else if (string.IsNullOrEmpty(userDTO.Password))
                return "Senha não pode ser vazia";
            else if (string.IsNullOrEmpty(userDTO.Name))
                return "Nome não pode ser vazio";

            if (!ValidateEmail(userDTO.Email))
                return "Email inválido: " + userDTO.Email;

            return "";
        }

        private static bool ValidateEmail(string email)
        {
            var validateEmailRegex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");

            if (validateEmailRegex.IsMatch(email))
                return true;

            return false;
        }
    } 
}