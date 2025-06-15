using AppUnitTest.Models;
using AppUnitTest.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AppUnitTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Retrieves all users from the system.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        /// <summary>
        /// Retrieves a user by their unique identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetUserById/{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                return Ok(user);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Creates a new user with the provided user object.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            try
            {
                var result = await _userService.CreateUserAsync(user);

                if (result > 0)
                    return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);

                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating user");
            }
            catch (ArgumentNullException ex)
            {
                return base.BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates an existing user with the provided user object.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            try
            {
                var result = await _userService.UpdateUserAsync(user);
                if (result > 0)
                    return NoContent();

                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating user");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a user by their unique identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                var result = await _userService.DeleteUserAsync(id);
                if (result > 0)
                    return NoContent();

                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting user");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
