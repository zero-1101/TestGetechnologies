using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TestGetechnologies.API.DbConfig;

namespace TestGetechnologies.API.DataAccess
{
    public interface IRepository<T>
    {
        Task<T?> GetById(int id);
        IQueryable<T> GetAll();
        Task<T> Create(T entity);
        void Update(T entity);
        Task<bool> Delete(int id);
    }

    public abstract class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        protected DbSet<T> Entities => _context.Set<T>();

        protected Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<T?> GetById(int id)
        {
            T? result = await Entities.FindAsync(id);
            return result;
        }

        public IQueryable<T> GetAll()
        {
            return Entities;
        }

        public async Task<T> Create(T entity)
        {
            EntityEntry<T> result = await Entities.AddAsync(entity);
            return result.Entity;
        }

        public void Update(T entity)
        {
            Entities.Update(entity);
        }

        public async Task<bool> Delete(int id)
        {
            T? entity = await GetById(id);
            
            if (entity is null)
            {
                return false;
            }

            Entities.Remove(entity);

            return true;
        }
    }
}
