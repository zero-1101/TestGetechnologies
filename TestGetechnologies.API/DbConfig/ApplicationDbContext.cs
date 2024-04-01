using Microsoft.EntityFrameworkCore;
using TestGetechnologies.API.DbConfig.Entities;

namespace TestGetechnologies.API.DbConfig
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Persona> Persona { get; set; }
        public DbSet<Factura> Factura { get; set; }
    }
}
