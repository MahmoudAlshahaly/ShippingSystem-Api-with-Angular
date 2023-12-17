using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.DAL.Data.Models
{
    public class Weight
    {
        public int Id { get; set; }
        public double DefaultWeight { get; set; }
        public double AdditionalPrice { get; set; }
    }
}
