using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AA2.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string Contrasena { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Rol { get; set; } // "Paciente" o "Admin"
        public bool EstaActivo { get; set; } // Indica si el usuario está activo o no;
         public ICollection<Cita> Citas { get; set; }
    }
}



