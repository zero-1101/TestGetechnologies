using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGetechnologies.Shared
{
    public record personaDetail(
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
}
