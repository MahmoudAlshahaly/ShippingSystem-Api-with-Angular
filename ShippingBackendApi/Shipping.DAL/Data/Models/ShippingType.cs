using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.DAL.Data.Models
{
    public class ShippingType
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Cost { get; set; }
        public bool status { get; set; } = true;
        public bool IsDeleted { get; set; }=false;

    }
}
