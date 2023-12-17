using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.DAL.Data.Models
{
    public class ReasonsRefusalType
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool isDeleted { get; set; } = false;
    }
}
