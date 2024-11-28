using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace cluster.Shared.Entities
{
    public class Tournament
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]
        [Display(Name = "Nombre del Torneo")]
        public string Name { get; set; } = null!;

        [Required]
        [Display(Name = "Fecha de Inicio")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "Fecha de Fin")]
        public DateTime EndDate { get; set; } 

        [Required]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]
        [Display(Name = "Ubicación")]
        public string Location { get; set; } = null!;

        [Required]
        [MaxLength(30, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]
        [Display(Name = "Estado")]
        public string Status { get; set; } = null!;

        public ICollection<Match> Matches { get; set; } = null!;
    }
}