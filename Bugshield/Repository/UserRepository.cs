using Bugshield.Interfaces;
using Bugshield.Models;
using Microsoft.EntityFrameworkCore;
#nullable disable
namespace Bugshield.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ProjectBugshieldContext _context;
        public UserRepository(ProjectBugshieldContext context)
        {
            _context = context;
        }
        /// <summary>
        /// To Get All users
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
        /// <summary>
        /// To get a specific user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<User> GetUserAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }
        /// <summary>
        /// To Create new User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task CreateUserAsync(User user)
        {
             user.Roleid = 3;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// To Update user details
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task UpdateUserDetailsAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// To Delete a User
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
        /// <summary>
        /// To Post Resigned User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task PostResignedUser(ResignedUser user)
        {
            _context.ResignedUsers.Update(user);
            await _context.SaveChangesAsync();
        }
    }

}
