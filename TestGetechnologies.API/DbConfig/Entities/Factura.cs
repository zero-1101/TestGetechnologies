using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TestGetechnologies.API.DbConfig.Entities
{
    public class Factura
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTimeOffset Fecha { get; set; }
        
        [Required, Range(0, int.MaxValue)]
        public Decimal Monto { get; set; }

        [Required]
        [ForeignKey(nameof(Entities.Persona))]
        public int PersonaId { get; set; }
        public Persona Persona { get; set; } = null!;
    }
}
