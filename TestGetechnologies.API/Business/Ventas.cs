using TestGetechnologies.API.DataAccess;
using TestGetechnologies.API.DbConfig.Entities;

namespace TestGetechnologies.API.Business
{
    public class Ventas
    {
        private readonly IFacturaRepository _facturaRepository;

        public Ventas(IFacturaRepository facturaRepository)
        {
            this._facturaRepository = facturaRepository;
        }

        public async Task<Factura?> GetById(int id)
        {
            Factura? factura = await _facturaRepository.GetById(id);
            return factura;
        }

        public int GetTotalRowsByPersona(int personaId) => 
            _facturaRepository.GetAll()
            .Where(f => f.PersonaId == personaId).Count();

        public List<Factura> FindFacturasByPersona(int personaId, int? pageNumber)
        {
            var facturas = _facturaRepository.GetAll()
                .Where(f => f.PersonaId == personaId);

            if (pageNumber is null)
            {
                return facturas.ToList();
            }

            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            int pageSize = Constants.PageSize;
            facturas = facturas.Skip(pageSize * (pageNumber.Value - 1)).Take(pageSize);

            return facturas.ToList();
        }

        public async Task<Factura?> CreateFactura(Factura factura)
        {
            if (factura is null)
            {
                return null;
            }

            Factura result = await _facturaRepository.Create(factura);
            return result;
        }

        public async Task<bool> UpdateFactura(Factura factura)
        {
            Factura facturaUpdate = await _facturaRepository.GetById(factura.Id);

            if (facturaUpdate is null)
            {
                return false;
            }

            facturaUpdate.Fecha = factura.Fecha;
            facturaUpdate.Monto = factura.Monto;

            _facturaRepository.Update(facturaUpdate);
            return true;
        }
    }
}
