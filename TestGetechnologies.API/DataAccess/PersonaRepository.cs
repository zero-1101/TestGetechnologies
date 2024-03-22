using Microsoft.EntityFrameworkCore;
using TestGetechnologies.API.DbConfig;
using TestGetechnologies.API.DbConfig.Entities;

namespace TestGetechnologies.API.DataAccess
{
    public class PersonaRepository : Repository<Persona>
    {
        public PersonaRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
