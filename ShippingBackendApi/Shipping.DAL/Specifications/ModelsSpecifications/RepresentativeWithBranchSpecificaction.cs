
using Microsoft.IdentityModel.Tokens;
using Shipping.DAL.Data.Models;
using Shipping.DAL.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.DAL.Specifications
{
    public class RepresentativeWithBranchSpecificaction : BaseSpecification<Representative>
    {
        public RepresentativeWithBranchSpecificaction(GSpecParams representativeSpecParams)
            : base(x => string.IsNullOrEmpty(representativeSpecParams.Search) || x.Name.ToLower().Contains(representativeSpecParams.Search))
           
        {
            AddIncludes(x => x.branch);
            AddOrderBy(x => x.Name);
            ApplyPaging(representativeSpecParams.PageSize * (representativeSpecParams.PageIndex -1), representativeSpecParams.PageSize);

            if (!string.IsNullOrEmpty(representativeSpecParams.Sort))
            {
                switch (representativeSpecParams.Sort)
                {
                    case "Desc":
                        AddOrderByDescending(p => p.Name); break;
                    default:
                        AddOrderBy(p => p.Name); break;
                }
            }
        }
        
    }
}
