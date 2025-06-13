using System.Text.Json.Serialization;

namespace AA2.Models
{
    public class CitaDtoOut
    {
        public int Id { get; set; }
        [JsonIgnore]
        public int IdUsuario { get; set; }
        [JsonIgnore]
        public int IdMedico { get; set; }
        public string NombrePaciente { get; set; }
        public string NombreMedico { get; set; }
        public DateTime FechaCita { get; set; }
        public string Motivo { get; set; }
        public bool Confirmada { get; set; }
    }
}