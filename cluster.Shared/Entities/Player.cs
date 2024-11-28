using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;


namespace cluster.Shared.Entities
{
    public class Player
    {
        public int Id { get; set; }

        [Required]
        public int TeamId { get; set; }

        [Required]
        public int UserId { get; set; }  // Add this for the foreign key

        [Required]
        [MaxLength(30, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]
        [Display(Name = "Tipo")]
        public string Type { get; set; } = null!;

        public Team Team { get; set; } = null!;
        public User User { get; set; } = null!;  // Add this navigation property
    }
}