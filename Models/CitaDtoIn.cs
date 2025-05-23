using System.ComponentModel.DataAnnotations;

namespace AA2.Models
{
    public class CitaDtoIn
    {
        [Required]
        public string NombrePaciente { get; set; }

        [Required]
        public string NombreMedico { get; set; }

        [Required]
        public DateTime FechaCita { get; set; }

        [Required]
        public string Motivo { get; set; }

        public bool? Confirmada { get; set; }
    }
}
