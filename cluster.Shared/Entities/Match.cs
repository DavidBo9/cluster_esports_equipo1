using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cluster.Shared.Entities
{
    public class Match
    {
        public int Id { get; set; }

        [Required]
        public int TournamentId { get; set; }

        [Required]
        public int Team1Id { get; set; }

        [Required]
        public int Team2Id { get; set; }

        [Required]
        [Display(Name = "Hora de Inicio")]
        public DateTime StartTime { get; set; }

        [Required]
        [Display(Name = "Hora de Fin")]
        public DateTime EndTime { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]
        [Display(Name = "Puntuación")]
        public string Score { get; set; }

        public Tournament Tournament { get; set; }
        public Team Team1 { get; set; }
        public Team Team2 { get; set; }
    }
}