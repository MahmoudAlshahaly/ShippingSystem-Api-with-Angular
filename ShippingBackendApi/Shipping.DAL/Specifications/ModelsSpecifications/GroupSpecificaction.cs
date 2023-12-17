
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
    public class GroupSpecificaction : BaseSpecification<Group>
    {
        public GroupSpecificaction(GSpecParams groupSpecParams)
            : base(x => (string.IsNullOrEmpty(groupSpecParams.Search) || x.Name.ToLower().Contains(groupSpecParams.Search)) &&
            ( x.IsDeleted == false ))
           
        {
           
            AddOrderBy(x => x.Date);
            ApplyPaging(groupSpecParams.PageSize * (groupSpecParams.PageIndex -1), groupSpecParams.PageSize);

            if (!string.IsNullOrEmpty(groupSpecParams.Sort))
            {
                switch (groupSpecParams.Sort)
                {
                    case "Desc":
                        AddOrderByDescending(p => p.Date); break;
                    default:
                        AddOrderBy(p => p.Date); break;
                }
            }
        }
        
    }
}
