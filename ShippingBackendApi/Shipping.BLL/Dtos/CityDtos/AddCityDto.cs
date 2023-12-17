using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.BLL.Dtos
{
    public class AddCityDto
    {
 
        public string Name { get; set; } 
        public double Price { get; set; }
        public double? Pickup { get; set; }
        public int GovernorateId { get; set; }

    }
}
