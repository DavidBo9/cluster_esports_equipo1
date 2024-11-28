using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cluster.Shared.Entities
{
    public class Team
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]
        [Display(Name = "Nombre del Equipo")]
        public string TeamName { get; set; } = null!;

        public ICollection<Player> Players { get; set; } = null!;
    }
}