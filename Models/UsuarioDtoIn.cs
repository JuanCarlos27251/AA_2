using System.ComponentModel.DataAnnotations;

namespace AA2.Models
{
        public class UsuarioDtoin
        {
                [Required]
                [StringLength(50)]
                public string Nombre { get; set; }

                [Required]
                [EmailAddress]
                public string Email { get; set; }

                [Required]
                [StringLength(20, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
                public string Contrasena { get; set; }

                public bool EstaActivo { get; set; } // Indica si el usuario está activo o no
        }

}

