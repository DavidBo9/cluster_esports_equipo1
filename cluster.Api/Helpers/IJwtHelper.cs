using cluster.Shared.Entities;
namespace cluster.Api.Helpers
{
    public interface IJwtHelper
    {
        string GenerateToken(User user, IList<string> roles);
    }
}