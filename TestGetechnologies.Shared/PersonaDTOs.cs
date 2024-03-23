using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGetechnologies.Shared
{
    public record CreatePersona(
        [Required(ErrorMessage = "El campo {0} es requerido"), MaxLength(256)]
        string Nombre,
        [Display(Name = "Apellido Paterno")]
        [Required(ErrorMessage = "El campo {0} es requerido"), MaxLength(256)]
        string ApellidoPaterno,
        [Display(Name = "Apellido Materno")]
        [MaxLength(256)]
        string? ApellidoMaterno,
        [Display(Name = "Identificación")]
        [Required(ErrorMessage = "El campo {0} es requerido"), MaxLength(256)]
        string Identificacion
        );

    public record UpdatePersona(
        [Required]
        int Id,
        [Required(ErrorMessage = "El campo {0} es requerido"), MaxLength(256)]
        string Nombre,
        [Display(Name = "Apellido Paterno")]
        [Required(ErrorMessage = "El campo {0} es requerido"), MaxLength(256)]
        string ApellidoPaterno,
        [Display(Name = "Apellido Materno")]
        [MaxLength(256)]
        string? ApellidoMaterno,
        [Display(Name = "Identificación")]
        [Required(ErrorMessage = "El campo {0} es requerido"), MaxLength(256)]
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
