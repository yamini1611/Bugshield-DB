using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bugshield.Models;
using Bugshield.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace Bugshield.Controllers
{
    [Authorize(Roles ="Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleRepository _repository;
        public RolesController(IRoleRepository repository)
        {
            _repository = repository;
        }
        /// <summary>
        /// To get All  Roles 
        /// </summary>
        /// <returns></returns>
        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            try
            {
                var roles = await _repository.GetAllRolesAsync();
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }  
        }
        /// <summary>
        /// To get Specific Role
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Roles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRole(int id)
        {
            var role = await _repository.GetRoleByIdAsync(id);
            if (role == null)
            {
                return Content("Role value is null");
            }

            return Ok(role);
        }
        /// <summary>
        /// To Update a Specific Role
        /// </summary>
        /// <param name="id"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        // PUT: api/Roles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(int id, Role role)
        {
            if (id != role.Roleid)
            {
                return BadRequest("id does not match");
            }
            try
            {
                await _repository.UpdateRoleAsync(role);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }
        /// <summary>
        /// To create a Role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        // POST: api/Roles
        [HttpPost]
        public async Task<ActionResult<Role>> PostRole(Role role)
        {
            if(role == null)
            {
                return BadRequest("role value is null");
            }
            try
            {
                await _repository.CreateRoleAsync(role);

            }
            catch(DbUpdateConcurrencyException ex)
            {
                return BadRequest(ex.Message);
            }
            return CreatedAtAction("GetRole", new { id = role.Roleid }, role);
        }

        // DELETE: api/Roles/5
        /// <summary>
        /// To Delete a Role
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            if (!await _repository.RoleExistsAsync(id))
            {
                return Content("id does not exists");
            }
            try
            {
                await _repository.DeleteRoleAsync(id);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Content("delete method executed");
        }
    }
}
