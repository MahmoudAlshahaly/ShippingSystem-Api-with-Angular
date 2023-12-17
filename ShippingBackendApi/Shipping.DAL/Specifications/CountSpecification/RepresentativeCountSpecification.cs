
using Shipping.DAL.Data.Models;
using Shipping.DAL.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.DAL.Specifications
{
    public class RepresentativeCountSpecification : BaseSpecification<Representative>
    {
        public RepresentativeCountSpecification(GSpecParams representativeSpecParams)
            :base(x=>string.IsNullOrEmpty(representativeSpecParams.Search) || x.Name.ToLower().Contains(representativeSpecParams.Search)) 
           
            {
            
        }
    }
}
