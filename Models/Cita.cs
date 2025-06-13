using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AA2.Models
{
    public class Cita
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }    // Relación con Usuario

        [ForeignKey("Medico")]
        public int IdMedico { get; set; }     // Relación con Medico
        public DateTime FechaCita { get; set; }

        [Required]
        [StringLength(100)]
        public string Motivo { get; set; }
        public bool Confirmada { get; set; }
        

        public string? NombrePaciente { get; set; }   // Se asigna al mostrar la cita
        public string? NombreMedico { get; set; }     // Se asigna al mostrar la cita
    }
}
