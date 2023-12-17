

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shipping.DAL.Data;
using Shipping.DAL.Specifications;
using System.Linq.Expressions;

namespace Shipping.DAL.Repositories;

public class TRepository<T> : IRepository<T> where T : class
{


    private readonly ShippingContext _context;
    private readonly DbSet<T> _dbSet;

    public TRepository(ShippingContext context )
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        var a=(_context.GetHashCode().ToString());
        return await _dbSet.FindAsync(id);
    }


    public async Task<T> GetByCriteriaAsync(Expression<Func<T, bool>> criteria, string[]? includes = null)
    {
        var query = _dbSet.AsQueryable().Where(criteria).AsNoTracking();
        if (includes != null)
        {
            foreach (var include in includes)
            {
                
                query = query.Include(include);
            }
        }
        var entity = await query.FirstOrDefaultAsync();
        return entity ;
    }




    public async Task<IQueryable<T>> GetAllAsync()
    {
        var a = (_context.GetHashCode().ToString());
      
        IQueryable<T> query = _dbSet;

        return await Task.FromResult(query);
    }


    public async Task<int> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        return await SaveChangesAsync();

    }

    public async Task<int> UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        return await SaveChangesAsync();
    }

    public async Task<int> DeleteAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity == null)
            return 0;

        _dbSet.Remove(entity);
        return await SaveChangesAsync();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }


    public async Task<IReadOnlyList<T>> GetAllDataAsync(ISpecification<T> specification)
    {
        return await ApplySpecification(specification).ToListAsync();
    }

    private IQueryable<T> ApplySpecification(ISpecification<T> specification)
    {
        return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), specification);
    }

    public async Task<int> CountAsync(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).CountAsync();
    }



}
