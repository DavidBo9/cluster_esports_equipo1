using System.ComponentModel.DataAnnotations;


namespace cluster.Shared.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]
        [Display(Name = "Usuario")]
        public string Username { get; set; } = null!;

        [Required]
        [EmailAddress]
        [Display(Name = "Correo Electrónico")]
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]
        [Display(Name = "Contraseña")]
        public string Password { get; set; } = null!;

        [Required]
        [MaxLength(20, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]
        [Display(Name = "Rol")]
        public string Role { get; set; } = null!;

        public Player? Player { get; set; }  // Add this navigation property
    }
}