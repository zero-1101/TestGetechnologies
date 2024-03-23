using TestGetechnologies.API.DbConfig;
using TestGetechnologies.API.DbConfig.Entities;

namespace TestGetechnologies.API.DataAccess
{
    public interface IFacturaRepository : IRepository<Factura>
    {
    }

    public class FacturaRepository : Repository<Factura>, IFacturaRepository
    {
        public FacturaRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
