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
        private readonly ILogger<DirectorioRestService> _logger;

        public DirectorioRestService(Directorio directorio, ILogger<DirectorioRestService> logger)
        {
            this._directorio = directorio;
            this._logger = logger;
        }

        [HttpGet("[action]")]
        public ActionResult<ResponseApi<PersonaDetailPaginated>> FindPersonas(int? pageNumber)
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
                    new ResponseApi<PersonaDetailPaginated>().CreateInternalServerErrorResponse());
            }
        }

        [HttpGet("[action]/{identificacion}")]
        public ActionResult<ResponseApi<PersonaDetail>> FindPersonaByIdentificacion(string identificacion)
        {
            try
            {
                if (string.IsNullOrEmpty(identificacion))
                {
                    return BadRequest(new ResponseApi<PersonaDetail>()
                        .CreateBadRequestResponse(new List<string> { "identificacion cannot be empty" }));
                }

                Persona? persona = _directorio.FindPersonaByIdentification(identificacion);

                if (persona is null)
                {
                    return NotFound(new ResponseApi<PersonaDetail>().CreateNotFoundResponse());
                }

                PersonaDetail personaDetail = new PersonaDetail(
                    Id: persona.Id,Nombre: persona.Nombre, ApellidoPaterno: persona.ApellidoPaterno,
                    ApellidoMaterno: persona.ApellidoMaterno, Identificacion: persona.Identificacion
                    );
                return Ok(new ResponseApi<PersonaDetail>().CreateSuccessResponse(personaDetail));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseApi<PersonaDetail>().CreateInternalServerErrorResponse());
            }
        }

        [HttpPost("[action]/{identificacion}")]
        public async Task<ActionResult<ResponseApi<bool>>> DeletePersonaByIdentificacion(string identificacion)
        {
            try
            {
                if (string.IsNullOrEmpty(identificacion))
                {
                    return BadRequest(new ResponseApi<bool>()
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
                    new ResponseApi<bool>().CreateInternalServerErrorResponse());
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<ResponseApi<PersonaDetail>>> CreatePersona(CreatePersona persona)
        {
            try
            {
                if (persona is null)
                {
                    _logger.LogWarning("Bad Request en Create Persona");

                    return BadRequest(new ResponseApi<PersonaDetail>()
                        .CreateBadRequestResponse(new List<string> { "persona cannot be empty" }));
                }

                if (ModelState.IsValid == false)
                {
                    List<string> errors = ModelState.Values
                        .SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();

                    _logger.LogWarning("Bad Request en Create Persona");

                    return BadRequest(new ResponseApi<PersonaDetail>().CreateBadRequestResponse(errors));
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

                _logger.LogInformation($"Persona creada correctamente");

                return Ok(new ResponseApi<PersonaDetail>().CreateSuccessResponse(personaDetail));
            }
            catch (Exception e)
            {
                _logger.LogError($"Error al crear Persona, ErrorMessage: {e.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseApi<PersonaDetail>().CreateInternalServerErrorResponse());
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<ResponseApi<bool>>> UpdatePersona(UpdatePersona persona)
        {
            try
            {
                if (persona is null)
                {
                    return BadRequest(new ResponseApi<bool>()
                        .CreateBadRequestResponse(new List<string> { "persona cannot be empty" }));
                }

                if (ModelState.IsValid == false)
                {
                    List<string> errors = ModelState.Values
                        .SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();

                    return BadRequest(new ResponseApi<bool>().CreateBadRequestResponse(errors));
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
                    return BadRequest(new ResponseApi<bool>()
                        .CreateBadRequestResponse(new List<string> { "The Persona Id is invalid or was not found" }));
                }

                return Ok(new ResponseApi<bool>().CreateSuccessResponse(true));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseApi<bool>().CreateInternalServerErrorResponse());
            }
        }
    }
}
