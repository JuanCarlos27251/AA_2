using System.ComponentModel.DataAnnotations;

namespace AA2.Models
{
    public class UsuarioDtoOut
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public DateTime FechaRegistro { get; set; }
        public string Rol { get; set; } // "Paciente" o "Admin"
        
    }
}