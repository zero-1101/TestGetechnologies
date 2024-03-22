using Microsoft.EntityFrameworkCore;
using TestGetechnologies.API.DbConfig.Entities;

namespace TestGetechnologies.API.DbConfig
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        DbSet<Persona> Persona { get; set; }
        DbSet<Factura> Factura { get; set; }
    }
}
