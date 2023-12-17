using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.BLL.Dtos
{
	public class GroupWithPermissionsDto
	{
	    public int Id { get; set; }
	    public string Name { get; set; }
		public DateTime Date { get; set; }
        public List<PermissionDto> Permissions { get; set; }

	}
}
