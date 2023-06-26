using After.hour.support.roaster.api.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace After.hour.support.roaster.api.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly DbSet<T> dbSet;

        public Repository(ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
            dbSet = _applicationDbContext.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {
            await dbSet.AddAsync(entity);

            await SaveAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true)
        {
            IQueryable<T> query = dbSet;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();

        }

        public async Task RemoveAsync(T entity)
        {
            dbSet.Remove(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
