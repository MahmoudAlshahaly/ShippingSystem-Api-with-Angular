
using Shipping.DAL.Data.Models;
using Shipping.DAL.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.DAL.Specifications
{
    public class EmployeeCountSpecification : BaseSpecification<Employee>
    {
        public EmployeeCountSpecification(GSpecParams employeeSpecParams)
            :base(x=>string.IsNullOrEmpty(employeeSpecParams.Search) || x.Name.ToLower().Contains(employeeSpecParams.Search)) 
           
            {
            
        }
    }
}
