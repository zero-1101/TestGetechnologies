using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGetechnologies.Shared
{
    public record CreatePersona(
        [Required, MaxLength(256)]
        string Nombre,
        [Required, MaxLength(256)]
        string ApellidoPaterno,
        [MaxLength(256)]
        string? ApellidoMaterno,
        [Required, MaxLength(256)]
        string Identificacion
        );

    public record UpdatePersona(
        [Required]
        int Id,
        [Required, MaxLength(256)]
        string Nombre,
        [Required, MaxLength(256)]
        string ApellidoPaterno,
        [MaxLength(256)]
        string? ApellidoMaterno,
        [Required, MaxLength(256)]
        string Identificacion
        );

    public record PersonaDetail(
        int Id,
        string Nombre,
        string ApellidoPaterno,
        string? ApellidoMaterno,
        string Identificacion
        );

    public record PersonaDetailPaginated(int totalRows, int pageNumber, int pageSize, List<PersonaDetail> personas);
}
