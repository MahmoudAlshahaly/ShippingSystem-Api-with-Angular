
using Shipping.DAL.Data.Models;
using Shipping.DAL.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.DAL.Specifications
{
    public class MerchantCountSpecification : BaseSpecification<Merchant>
    {
        public MerchantCountSpecification(GSpecParams merchantSpecParams)
            :base(x=>string.IsNullOrEmpty(merchantSpecParams.Search) || x.Name.ToLower().Contains(merchantSpecParams.Search)) 
           
            {
            
        }
    }
}
