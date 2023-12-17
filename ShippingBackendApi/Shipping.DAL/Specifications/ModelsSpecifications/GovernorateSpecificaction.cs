
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
    public class GovernorateSpecificaction : BaseSpecification<Governorate>
    {

        public GovernorateSpecificaction(GSpecParams SpecParams)
              : base(x => (string.IsNullOrEmpty(SpecParams.Search) || x.Name.ToLower().Contains(SpecParams.Search))
           && (x.IsDeleted == false))
        {
            
            AddOrderBy(x => x.Name);
            ApplyPaging(SpecParams.PageSize * (SpecParams.PageIndex -1), SpecParams.PageSize);

            if (!string.IsNullOrEmpty(SpecParams.Sort))
            {
                switch (SpecParams.Sort)
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
