using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.BLL.Dtos
{
    public class ShowBranchDto
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public bool isDeleted { get; set; }
        public  bool status { get; set; }
        public DateTime DateTime { get; set; }

    }
}
