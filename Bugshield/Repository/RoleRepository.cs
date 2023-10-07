#nullable disable
using Microsoft.EntityFrameworkCore;
using Bugshield.Models;

namespace Bugshield.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ProjectBugshieldContext _context;
        public RoleRepository(ProjectBugshieldContext context)
        {
            _context = context;
        }
        /// <summary>
        /// To Get all Roles
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return await _context.Roles.ToListAsync();
        }
        /// <summary>
        /// To get a specific Role
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Role> GetRoleByIdAsync(int id)
        {
            return await _context.Roles.FindAsync(id);
        }
        /// <summary>
        /// To Create a Role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task CreateRoleAsync(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Update Role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task UpdateRoleAsync(Role role)
        {
            _context.Entry(role).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Delete a Role
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteRoleAsync(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role != null)
            {
                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();
            }
        }
        /// <summary>
        /// To Check if a Role exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> RoleExistsAsync(int id)
        {
            return await _context.Roles.AnyAsync(e => e.Roleid == id);
        }
    }
}
