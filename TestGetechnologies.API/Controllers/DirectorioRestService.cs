using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TestGetechnologies.API.Business;
using TestGetechnologies.API.DbConfig.Entities;
using TestGetechnologies.Shared;

namespace TestGetechnologies.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorioRestService : ControllerBase
    {
        private readonly Directorio _directorio;

        public DirectorioRestService(Directorio directorio)
        {
            this._directorio = directorio;
        }

        [HttpGet("[action]")]
        public IActionResult FindPersonas(int? pageNumber)
        {
            try
            {
                List<PersonaDetail> personas = _directorio.FindPersonas(pageNumber).Select(pd => new PersonaDetail
                (
                    Id: pd.Id,
                    Nombre: pd.Nombre,
                    ApellidoPaterno: pd.ApellidoPaterno,
                    ApellidoMaterno: pd.ApellidoMaterno,
                    Identificacion: pd.Identificacion
                )).ToList();

                int TotalRows = _directorio.GetTotalRows();

                if (pageNumber is not null)
                {
                    pageNumber = pageNumber <= 0 ? 1 : pageNumber;
                }
                int pageSize = pageNumber is not null ? Constants.PageSize : TotalRows;
                PersonaDetailPaginated personaDetailPaginated = new 
                    PersonaDetailPaginated(TotalRows, pageNumber ?? 1, pageSize, personas);

                return Ok(new ResponseApi<PersonaDetailPaginated>().CreateSuccessResponse(personaDetailPaginated));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseApi<bool?>().CreateInternalServerErrorResponse());
            }
        }

        [HttpGet("[action]/{identificacion}")]
        public IActionResult FindPersonaByIdentificacion(string identificacion)
        {
            try
            {
                if (string.IsNullOrEmpty(identificacion))
                {
                    return BadRequest(new ResponseApi<List<string>>()
                        .CreateBadRequestResponse(new List<string> { "identificacion cannot be empty" }));
                }

                Persona? persona = _directorio.FindPersonaByIdentification(identificacion);

                if (persona is null)
                {
                    return NotFound(new ResponseApi<PersonaDetail?>().CreateNotFoundResponse());
                }

                PersonaDetail personaDetail = new PersonaDetail(
                    Id: persona.Id,Nombre: persona.Nombre, ApellidoPaterno: persona.ApellidoPaterno,
                    ApellidoMaterno: persona.ApellidoMaterno, Identificacion: persona.Identificacion
                    );
                return Ok(new ResponseApi<PersonaDetail?>().CreateSuccessResponse(personaDetail));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseApi<bool?>().CreateInternalServerErrorResponse());
            }
        }

        [HttpPost("[action]/{identificacion}")]
        public async Task<IActionResult> DeletePersonaByIdentificacion(string identificacion)
        {
            try
            {
                if (string.IsNullOrEmpty(identificacion))
                {
                    return BadRequest(new ResponseApi<List<string>>()
                        .CreateBadRequestResponse(new List<string> { "identificacion cannot be empty"}));
                }

                bool result = await _directorio.DeletePersonaByIdentification(identificacion);
                if (result == false)
                {
                    return NotFound(new ResponseApi<bool>().CreateNotFoundResponse());
                }
                return Ok(new ResponseApi<bool>().CreateSuccessResponse(true));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseApi<bool?>().CreateInternalServerErrorResponse());
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreatePersona(CreatePersona persona)
        {
            try
            {
                if (persona is null)
                {
                    return BadRequest(new ResponseApi<List<string>>()
                        .CreateBadRequestResponse(new List<string> { "persona cannot be empty" }));
                }

                if (ModelState.IsValid == false)
                {
                    List<string> errors = ModelState.Values
                        .SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();

                    return BadRequest(new ResponseApi<List<string>>().CreateBadRequestResponse(errors));
                }

                Persona personaCreate = new Persona
                {
                    Nombre = persona.Nombre,
                    ApellidoPaterno = persona.ApellidoPaterno,
                    ApellidoMaterno = persona.ApellidoMaterno,
                    Identificacion = persona.Identificacion,
                };

                Persona? result = await _directorio.CreatePersona(personaCreate);

                PersonaDetail personaDetail = new PersonaDetail(
                    Id: result.Id,Nombre: result.Nombre, ApellidoPaterno: result.ApellidoPaterno,
                    ApellidoMaterno: result.ApellidoMaterno, Identificacion: result.Identificacion
                    );

                return Ok(new ResponseApi<PersonaDetail>().CreateSuccessResponse(personaDetail));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseApi<bool?>().CreateInternalServerErrorResponse());
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UpdatePersona(UpdatePersona persona)
        {
            try
            {
                if (persona is null)
                {
                    return BadRequest(new ResponseApi<List<string>>()
                        .CreateBadRequestResponse(new List<string> { "persona cannot be empty" }));
                }

                if (ModelState.IsValid == false)
                {
                    List<string> errors = ModelState.Values
                        .SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();

                    return BadRequest(new ResponseApi<List<string>>().CreateBadRequestResponse(errors));
                }

                Persona personaUpdate = new Persona
                {
                    Id = persona.Id,
                    Nombre = persona.Nombre,
                    ApellidoPaterno = persona.ApellidoPaterno,
                    ApellidoMaterno = persona.ApellidoMaterno,
                    Identificacion = persona.Identificacion,
                };

                bool result = await _directorio.UpdatePersona(personaUpdate);

                if (result == false)
                {
                    return BadRequest(new ResponseApi<List<string>>()
                        .CreateBadRequestResponse(new List<string> { "The Persona Id is invalid or was not found" }));
                }

                return Ok(new ResponseApi<bool>().CreateSuccessResponse(true));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseApi<bool?>().CreateInternalServerErrorResponse());
            }
        }
    }
}
