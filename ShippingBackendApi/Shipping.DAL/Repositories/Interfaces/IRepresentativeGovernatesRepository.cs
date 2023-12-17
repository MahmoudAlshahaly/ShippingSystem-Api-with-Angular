using Shipping.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.DAL.Repositories
{
    public interface IRepresentativeGovernatesRepository
    {
        Task<IQueryable<RepresentativeGovernate>> GetRepresentativeGovernatesByRepresentativeId(string Id);
        Task<int> AddRangeAsync(List<RepresentativeGovernate> representativeGovernates);
        Task<int> RemoveRangeAsync(List<RepresentativeGovernate> representativeGovernates);
    }

}

