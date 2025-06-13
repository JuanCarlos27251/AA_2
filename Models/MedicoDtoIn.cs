using System.ComponentModel.DataAnnotations;

namespace AA2.Models
{

    public class MedicoDtoIn
    {
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50)]
        public string Especialidad { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string Telefono { get; set; }

        public bool Disponible { get; set; }
    }

}

