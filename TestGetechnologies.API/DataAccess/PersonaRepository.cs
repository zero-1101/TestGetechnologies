using Microsoft.EntityFrameworkCore;
using TestGetechnologies.API.DbConfig;
using TestGetechnologies.API.DbConfig.Entities;

namespace TestGetechnologies.API.DataAccess
{
    public interface IPersonaRepository : IRepository<Persona>
    {

    }

    public class PersonaRepository : Repository<Persona>, IPersonaRepository
    {
        public PersonaRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
