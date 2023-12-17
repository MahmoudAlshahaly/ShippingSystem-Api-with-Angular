using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.BLL.Dtos
{
    public class UpdatShippingTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public double Cost { get; set; }
    }
}
