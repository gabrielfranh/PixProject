using Microsoft.AspNetCore.Mvc;
using UserAPI.DTOs;
using UserAPI.Services.Interfaces;

namespace UserAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
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
            if (userDTO is null)
                return BadRequest(userDTO);

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
    } 
}