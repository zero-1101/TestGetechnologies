using TestGetechnologies.API.DataAccess;
using TestGetechnologies.API.DbConfig.Entities;

namespace TestGetechnologies.API.Business
{
    public class Ventas
    {
        private readonly FacturaRepository _facturaRepository;

        public Ventas(FacturaRepository facturaRepository)
        {
            this._facturaRepository = facturaRepository;
        }

        public List<Factura> FindFacturasByPersona(int personaId)
        {
            var facturas = _facturaRepository.GetAll()
                .Where(f => f.PersonaId == personaId).ToList();
            return facturas;
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

        public void UpdateFactura(Factura factura)
        {
            _facturaRepository.Update(factura);
        }
    }
}
