using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.BLL.Dtos
{
    public class ReadCityDto
    {
        public int id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double? Pickup { get; set; }
        public bool IsDeleted { get; set; }
    }
}
