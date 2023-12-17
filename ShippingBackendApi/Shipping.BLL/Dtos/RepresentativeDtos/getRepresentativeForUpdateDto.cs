using Shipping.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.BLL.Dtos
{
    public class getRepresentativeForUpdateDto
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public int BranchId { get; set; }
        public string Address { get; set; }
        public double Amount { get; set; }
        public AmountType Type { get; set; }

        public List<GovernatesDto> RepresentativeGovernates { get; set; }
    }

}
