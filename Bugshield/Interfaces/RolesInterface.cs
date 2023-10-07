using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bugshield.Models;

namespace Bugshield.Repositories
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAllRolesAsync();
        Task<Role> GetRoleByIdAsync(int id);
        Task CreateRoleAsync(Role role);
        Task UpdateRoleAsync(Role role);
        Task DeleteRoleAsync(int id);
        Task<bool> RoleExistsAsync(int id);
    }
}
