using cluster.Shared.Entities;

namespace cluster.Api.Helpers
{
    public interface IUserHelper
    {
        Task<User?> GetUserAsync(string email);
        Task<User?> GetUserAsync(int id);
        Task<bool> IsUserInRoleAsync(User user, string roleName);
        Task<bool> AddUserToRoleAsync(User user, string roleName);
        Task CheckRoleAsync(string roleName);
    }
}