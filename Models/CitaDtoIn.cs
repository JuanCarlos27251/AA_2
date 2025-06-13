using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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


        [JsonIgnore]
        public int IdUsuario { get; set; }
        [JsonIgnore]
        public int IdMedico { get; set; }
    
    }
}
