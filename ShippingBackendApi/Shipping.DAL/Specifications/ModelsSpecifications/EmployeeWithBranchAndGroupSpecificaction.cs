
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
    public class EmployeeWithBranchAndGroupSpecificaction : BaseSpecification<Employee>
    {
        public EmployeeWithBranchAndGroupSpecificaction(GSpecParams employeeSpecParams)
            : base(x => string.IsNullOrEmpty(employeeSpecParams.Search) || x.Name.ToLower().Contains(employeeSpecParams.Search))
           
        {
            AddIncludes(x => x.branch);
            AddIncludes(x => x.Group);
            AddOrderBy(x => x.Name);
            ApplyPaging(employeeSpecParams.PageSize * (employeeSpecParams.PageIndex -1), employeeSpecParams.PageSize);

            if (!string.IsNullOrEmpty(employeeSpecParams.Sort))
            {
                switch (employeeSpecParams.Sort)
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
