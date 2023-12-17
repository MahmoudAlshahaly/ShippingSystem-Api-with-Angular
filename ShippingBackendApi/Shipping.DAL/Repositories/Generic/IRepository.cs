using Microsoft.EntityFrameworkCore;
using Shipping.DAL.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.DAL.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<T> GetByCriteriaAsync(Expression<Func<T, bool>> criteria, string[]? includes = null);
        Task<IQueryable<T>> GetAllAsync();
        Task<int> AddAsync(T entity);
        Task<int> UpdateAsync(T entity);
        Task<int> DeleteAsync(int id);
        Task<int> SaveChangesAsync();
        Task<IReadOnlyList<T>> GetAllDataAsync(ISpecification<T> specification);
        Task<int> CountAsync(ISpecification<T> spec);
       
    }
}
