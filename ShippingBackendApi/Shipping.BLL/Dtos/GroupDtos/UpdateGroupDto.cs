using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.BLL.Dtos
{
    public class UpdateGroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<GropPermissionAddDto> groupPermissions { get; set; }
    }
}
