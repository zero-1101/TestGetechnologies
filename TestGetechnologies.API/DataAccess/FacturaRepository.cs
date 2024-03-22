using TestGetechnologies.API.DbConfig;
using TestGetechnologies.API.DbConfig.Entities;

namespace TestGetechnologies.API.DataAccess
{
    public class FacturaRepository : Repository<Factura>
    {
        public FacturaRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
