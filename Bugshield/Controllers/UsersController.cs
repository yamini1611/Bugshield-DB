using Microsoft.AspNetCore.Mvc;
using Bugshield.Models;
using Bugshield.Interfaces;
using Microsoft.AspNetCore.Authorization;
#nullable disable
namespace Bugshield.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        /// <summary>
        /// To get All User details
        /// </summary>
        /// <returns></returns>
        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _userRepository.GetUsersAsync();
            return Ok(users);
        }
        /// <summary>
        /// To get a specific User's details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            if (id == 0)
            {
                return Content("Id value is 0");
            }
            var user = await _userRepository.GetUserAsync(id);

            if (user == null)
            {
                return Content("User value is null");
            }
            return Ok(user);
        }
        /// <summary>
        /// Update User details
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="updatedUser"></param>
        /// <returns></returns>
        // PUT: api/Users/5
        [HttpPut("details/{userId}")]
        public async Task<IActionResult> PutUserDetails(int userId, [FromBody] User updatedUser)
        {
            var existingUser = await _userRepository.GetUserAsync(userId);

            if (existingUser == null)
            {
                return Content("Passed value is null");
            }
            try
            {
                existingUser.Username = updatedUser.Username;
                existingUser.Password = updatedUser.Password;
                existingUser.Phone = updatedUser.Phone;
                existingUser.Email = updatedUser.Email;
                existingUser.Roleid = updatedUser.Roleid;
                existingUser.Computerid = updatedUser.Computerid;
                await _userRepository.UpdateUserDetailsAsync(existingUser);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// To create a new User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] User user)
        {
            try
            {
                await _userRepository.CreateUserAsync(user);
                return Ok("created Successfully");
            }
            catch
            {
                return BadRequest("Error Registering User");
            }
        }
        /// <summary>
        /// To delete a particular user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (id == 0)
            {
                return Content("Id value is 0");
            }
            try
            {
                await _userRepository.DeleteUserAsync(id);
                return Ok("Deleted Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// To Post the Deleted User to Resigned User Table
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("PostResigned")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ResignedUser(ResignedUser user)
        {
            try
            {
                await _userRepository.PostResignedUser(user);
                return Ok("created Successfully");
            }
            catch
            {
                return BadRequest("Error Registering User");
            }
        }
    }
}
