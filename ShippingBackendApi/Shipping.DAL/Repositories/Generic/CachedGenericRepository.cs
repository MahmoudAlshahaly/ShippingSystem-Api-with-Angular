using Microsoft.Extensions.Caching.Memory;
using Shipping.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.DAL.Repositories.Generic
{
    public class CachedGenericRepository<T> : ICachedGenericRepository<T> where T : class
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IRepository<T> _repository;

        public CachedGenericRepository(IMemoryCache memoryCache, IRepository<T> repository)
        {
            _memoryCache = memoryCache;
            _repository = repository;
        }
        public async Task<List<T>> GetAllCachedAsync(string cacheKey)
        {
            //var g = _memoryCache.Get(cacheKey);
            _memoryCache.
            if (!_memoryCache.TryGetValue(cacheKey, out List<T>? cachedData))
            {
                cachedData = await _repository.GetAllNOWAsync();
                _memoryCache.Set(cacheKey, cachedData, TimeSpan.FromMinutes(10));
            }
            return cachedData;
        }
        public async Task RemoveCache(string cacheKey)
        {
            _memoryCache.Remove(cacheKey);
        }
        public Task<T> GetCachedByCriteriaAsync(Expression<Func<T, bool>> criteria, string[]? includes = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetCachedByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
