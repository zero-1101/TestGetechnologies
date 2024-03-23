using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGetechnologies.Shared
{
    public record CreateFactura(
        [Required]
        DateTimeOffset Fecha,
        [Required, Range(0, int.MaxValue)]
        Decimal Monto,
        [Required]
        int PersonaId
        );

    public record UpdateFactura(
        [Required]
        int Id,
        [Required]
        DateTimeOffset Fecha,
        [Required, Range(0, int.MaxValue)]
        Decimal Monto
        );

    public record FacturaDetail(
        int Id,
        DateTimeOffset Fecha,
        Decimal Monto,
        int PersonaId
        );

    public record FacturaDetailPaginated(int totalRows, int pageNumber, int pageSize, List<FacturaDetail> facturas);
}
