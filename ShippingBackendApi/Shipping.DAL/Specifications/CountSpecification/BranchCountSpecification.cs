
using Shipping.DAL.Data.Models;
using Shipping.DAL.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.DAL.Specifications
{
    public class BranchCountSpecification : BaseSpecification<Branch>
    {
        public BranchCountSpecification(GSpecParams SpecParams)
            :base(x=>(string.IsNullOrEmpty(SpecParams.Search) || x.Name.ToLower().Contains(SpecParams.Search))
           &&(x.isDeleted == false))
            {
            
        }
    }
}
