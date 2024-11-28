using cluster.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace cluster.Api.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly DataContext _context;

        public UserHelper(DataContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User> GetUserAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> IsUserInRoleAsync(User user, string roleName)
        {
            return user.Role == roleName;
        }

        public async Task<bool> AddUserToRoleAsync(User user, string roleName)
        {
            user.Role = roleName;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task CheckRoleAsync(string roleName)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Role == roleName);
            if (user == null)
            {
                // Role doesn't exist, create a dummy user with this role
                await _context.Users.AddAsync(new User
                {
                    Username = $"System_{roleName}",
                    Email = $"system_{roleName.ToLower()}@tournament.com",
                    Password = "placeholder",
                    Role = roleName
                });
                await _context.SaveChangesAsync();
            }
        }
    }
}