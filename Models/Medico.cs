using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AA2.Models
{
    public class Medico
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50)]
        public string Especialidad { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaAlta { get; set; }
        public bool Disponible { get; set; }
        public ICollection<Cita> Citas { get; set; }
    }
}
