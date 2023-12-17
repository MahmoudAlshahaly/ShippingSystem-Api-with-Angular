using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.BLL.Dtos
{
    public class ReadGovernoratesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ReadCityDto> Cities { get; set; }
    }
}
