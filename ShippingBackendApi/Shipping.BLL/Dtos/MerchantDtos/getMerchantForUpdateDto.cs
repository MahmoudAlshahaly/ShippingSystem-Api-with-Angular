using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.BLL.Dtos
{
    public class getMerchantForUpdateDto
    { 
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string StoreName { get; set; }
        public double? PickUp { get; set; }
        public double ReturnerPercent { get; set; }
        public int BranchId { get; set; }
        public int CityId { get; set; }
        public int GovernorateId { get; set; }
        public List<SpecialPriceDto> SpecialPrices { get; set; }
    }
}
