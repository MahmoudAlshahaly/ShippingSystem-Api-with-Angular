using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.BLL.Dtos
{
    public class ShowShippingTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Cost { get; set; }
        public bool IsDeleted { get; set; }
        public bool Status { get; set; }

    }
}
