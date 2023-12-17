using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.BLL.Dtos
{
    public class GetAllEmployeesDto
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }


        public string? BranchName { get; set; }
        public string? GroupName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
