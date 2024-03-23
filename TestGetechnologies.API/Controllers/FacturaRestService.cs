using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestGetechnologies.API.Business;
using TestGetechnologies.API.DbConfig.Entities;
using TestGetechnologies.Shared;

namespace TestGetechnologies.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaRestService : ControllerBase
    {
        private readonly Ventas _ventas;
        private readonly Directorio _directorio;

        public FacturaRestService(Ventas ventas, Directorio directorio)
        {
            this._ventas = ventas;
            this._directorio = directorio;
        }

        [HttpGet("[action]/{PersonaId}")]
        public ActionResult<ResponseApi<FacturaDetailPaginated>> FindFacturasByPersona(int? PersonaId, int? pageNumber)
        {
            try
            {
                if (PersonaId is null)
                {
                    return BadRequest(new ResponseApi<FacturaDetailPaginated?>()
                            .CreateBadRequestResponse(new List<string> { "PersonaId cannot be empty" }));
                }

                List<FacturaDetail> facturas = _ventas.FindFacturasByPersona(PersonaId.Value, pageNumber)
                    .Select(fd => new FacturaDetail(
                        Id: fd.Id,
                        Fecha : fd.Fecha,
                        Monto : fd.Monto,
                        PersonaId : fd.PersonaId
                        )).ToList();

                int TotalRows = _ventas.GetTotalRowsByPersona(PersonaId.Value);
                if (pageNumber is not null)
                {
                    pageNumber = pageNumber <= 0 ? 1 : pageNumber;
                }
                int pageSize = pageNumber is not null ? Constants.PageSize : TotalRows;
                FacturaDetailPaginated personaDetailPaginated = new 
                    FacturaDetailPaginated(TotalRows, pageNumber ?? 1, pageSize, facturas);

                return Ok(new ResponseApi<FacturaDetailPaginated>().CreateSuccessResponse(personaDetailPaginated));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseApi<FacturaDetailPaginated?>().CreateInternalServerErrorResponse());
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<ResponseApi<FacturaDetail>>> CreateFactura(CreateFactura factura)
        {
            try
            {
                if (factura is null)
                {
                    return BadRequest(new ResponseApi<FacturaDetail>()
                        .CreateBadRequestResponse(new List<string> { "factura cannot be empty" }));
                }

                if (ModelState.IsValid == false)
                {
                    List<string> errors = ModelState.Values
                        .SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();

                    return BadRequest(new ResponseApi<FacturaDetail>().CreateBadRequestResponse(errors));
                }

                Persona? personaExists = await _directorio.GetById(factura.PersonaId); 

                if (personaExists is null)
                {
                    return BadRequest(new ResponseApi<FacturaDetail>()
                        .CreateBadRequestResponse(new List<string> { "The Persona Id is invalid or was not found" }));
                }

                Factura facturaCreate = new Factura
                {
                    Fecha = factura.Fecha,
                    Monto = factura.Monto,
                    PersonaId = factura.PersonaId,
                };

                Factura? result = await _ventas.CreateFactura(facturaCreate);

                FacturaDetail facturaDetail = new FacturaDetail(result.Id, result.Fecha, result.Monto, result.PersonaId);

                return Ok(new ResponseApi<FacturaDetail>().CreateSuccessResponse(facturaDetail));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseApi<FacturaDetail>().CreateInternalServerErrorResponse());
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<ResponseApi<bool>>> UpdateFactura(UpdateFactura factura)
        {
            try
            {
                if (factura is null)
                {
                    return BadRequest(new ResponseApi<bool>()
                        .CreateBadRequestResponse(new List<string> { "factura cannot be empty" }));
                }

                if (ModelState.IsValid == false)
                {
                    List<string> errors = ModelState.Values
                        .SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();

                    return BadRequest(new ResponseApi<bool>().CreateBadRequestResponse(errors));
                }
                
                Factura? facturaExists = await _ventas.GetById(factura.Id);

                Factura facturaUpdate = new Factura
                {
                    Id = factura.Id,
                    Fecha = factura.Fecha,
                    Monto = factura.Monto,
                };

                bool result = await _ventas.UpdateFactura(facturaUpdate);

                if (result == false)
                {
                    return BadRequest(new ResponseApi<bool>()
                        .CreateBadRequestResponse(new List<string> { "The Factura Id is invalid or was not found" }));
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
