using Shipping.DAL.Data;
using Shipping.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.DAL.Repositories
{
    public class SpecialPricesRepository : TRepository<SpecialPrice>, ISpecialPricesRepository
    {
        private readonly ShippingContext _context;

        public SpecialPricesRepository(ShippingContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<SpecialPrice>> GetSpecialPricesByMerchantId(string Id)
        {
            return _context.SpecialPrices.Where(s => s.MerchentId == Id).ToList();
        }

        public async Task<int> AddRangeAsync(List<SpecialPrice> specialPrices)
        {
            if (specialPrices == null || specialPrices.Count == 0)
            {
                return 0;
            }

            await _context.SpecialPrices.AddRangeAsync(specialPrices);
            return await SaveChangesAsync();
        }

        public async Task<int> RemoveRangeAsync(List<SpecialPrice> specialPrices)
        {
            if (specialPrices == null || specialPrices.Count == 0)
            {
                return 0;
            }

            _context.SpecialPrices.RemoveRange(specialPrices);
            return await SaveChangesAsync();
        }
    }
}

