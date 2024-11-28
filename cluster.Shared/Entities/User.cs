using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace cluster.Shared.Entities
{
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(20)]
        public string Role { get; set; } = null!;

        public virtual Player? Player { get; set; }
    }
}