using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.BLL.Dtos
{
    public class EmployeeUpdateDto
    {
            public string Id { get; set; }
            public string Name { get; set; }
            public string PhoneNumber { get; set; }
            public int BranchId { get; set; }
            public string Address { get; set; }
            public int GroupId { get; set; }

    }
}
