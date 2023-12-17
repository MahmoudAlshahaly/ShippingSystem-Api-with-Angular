
using Shipping.DAL.Data.Models;
using Shipping.DAL.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.DAL.Specifications
{
    public class GroupCountSpecification : BaseSpecification<Group>
    {
        public GroupCountSpecification(GSpecParams groupSpecParams)
             : base(x => (string.IsNullOrEmpty(groupSpecParams.Search) || x.Name.ToLower().Contains(groupSpecParams.Search)) &&
            (x.IsDeleted == false))

        { 

        }
}
}
