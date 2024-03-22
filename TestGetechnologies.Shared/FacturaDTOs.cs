using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGetechnologies.Shared
{
    public record FacturaDetail(
        int Id,
        [Required]
        DateTimeOffset Fecha,
        [Required, Range(0, int.MaxValue)]
        Decimal Monto
        );
}
