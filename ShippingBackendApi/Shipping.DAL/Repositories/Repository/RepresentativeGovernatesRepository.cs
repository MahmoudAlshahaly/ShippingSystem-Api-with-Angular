using Microsoft.EntityFrameworkCore;
using Shipping.DAL.Data;
using Shipping.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.DAL.Repositories
{
    public class RepresentativeGovernatesRepository:TRepository<RepresentativeGovernate>,IRepresentativeGovernatesRepository
    {

        private readonly ShippingContext _context;

        public RepresentativeGovernatesRepository(ShippingContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IQueryable<RepresentativeGovernate>> GetRepresentativeGovernatesByRepresentativeId(string Id)
        {
            var representativeGovernates = _context.RepresentativeGovernates
                .Where(s => s.RepresentativeId == Id);

            return await Task.FromResult(representativeGovernates);
        }



        public async Task<int> AddRangeAsync(List<RepresentativeGovernate> representativeGovernates)
        {
            if (representativeGovernates == null || representativeGovernates.Count == 0)
            {
                return 0;
            }

            await _context.RepresentativeGovernates.AddRangeAsync(representativeGovernates);
            return await SaveChangesAsync();
        }

        public async Task<int> RemoveRangeAsync(List<RepresentativeGovernate> representativeGovernates)
        {
            if (representativeGovernates == null || representativeGovernates.Count == 0)
            {
                return 0;
            }

            _context.RepresentativeGovernates.RemoveRange(representativeGovernates);
            return await SaveChangesAsync();
        }



    }
}
