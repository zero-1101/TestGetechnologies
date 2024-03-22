using System.ComponentModel.DataAnnotations;

namespace TestGetechnologies.API.DbConfig.Entities
{
    public class Persona
    {
        [Required]
        public int Id { get; set; }

        [Required, MaxLength(256)]
        public string Nombre { get; set; }

        [Required, MaxLength(256)]
        public string ApellidoPaterno { get; set; }

        [MaxLength(256)]
        public string? ApellidoMaterno { get; set; }

        [Required, MaxLength(256)]
        public string Identificacion { get; set; }

        public ICollection<Factura> Facturas { get; set; } = new List<Factura>();
    }
}
