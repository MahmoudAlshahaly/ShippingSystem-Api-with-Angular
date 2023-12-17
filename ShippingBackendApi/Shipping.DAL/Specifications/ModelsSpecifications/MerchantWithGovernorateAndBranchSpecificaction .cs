
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
    public class MerchantWithGovernorateAndBranchSpecificaction : BaseSpecification<Merchant>
    {
        public MerchantWithGovernorateAndBranchSpecificaction(GSpecParams merchantSpecParams)
            : base(x => string.IsNullOrEmpty(merchantSpecParams.Search) || x.Name.ToLower().Contains(merchantSpecParams.Search))
           
        {
            AddIncludes(x => x.Governorate);
            AddIncludes(x => x.branch);
            AddOrderBy(x => x.Name);
            ApplyPaging(merchantSpecParams.PageSize * (merchantSpecParams.PageIndex -1), merchantSpecParams.PageSize);

            if (!string.IsNullOrEmpty(merchantSpecParams.Sort))
            {
                switch (merchantSpecParams.Sort)
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
