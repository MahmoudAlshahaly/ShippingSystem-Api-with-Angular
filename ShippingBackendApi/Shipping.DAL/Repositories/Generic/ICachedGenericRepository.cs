using System.Linq.Expressions;

namespace Shipping.DAL.Repositories.Interfaces
{
    public interface ICachedGenericRepository<T> where T : class
    {
        Task<T> GetCachedByIdAsync(int id);
        Task RemoveCache(string cacheKey);

        Task<T> GetCachedByCriteriaAsync(Expression<Func<T, bool>> criteria, string[]? includes = null);
        Task<List<T>> GetAllCachedAsync(string cacheKey);
    }
}
