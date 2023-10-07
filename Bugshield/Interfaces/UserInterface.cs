using Bugshield.Models;

namespace Bugshield.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserAsync(int id);
        Task UpdateUserDetailsAsync(User user);
        Task CreateUserAsync(User user);
        Task DeleteUserAsync(int id);
        Task PostResignedUser(ResignedUser user);
    }

}
