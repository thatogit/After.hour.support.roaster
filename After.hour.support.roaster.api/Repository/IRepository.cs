using System.Linq.Expressions;

namespace After.hour.support.roaster.api.Repository
{
    public interface IRepository<T> where T : class
    {
        Task CreateAsync(T entity);
        Task RemoveAsync(T entity);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);
        Task<T> GetAsync(Expression<Func<T, bool>> filter = null,bool tracked = true);
        Task SaveAsync();
    }
}
