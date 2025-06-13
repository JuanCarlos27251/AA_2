using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace AA2.Models
{
    public class MedicoDtoOut
    {
        [JsonIgnore] // Esta propiedad no se serializará en la respuesta JSON
        public int Id { get; set; }
        
        public string Nombre { get; set; }
        public string Especialidad { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaAlta { get; set; }
        public bool Disponible { get; set; }
    }

}

