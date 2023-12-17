using Shipping.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.BLL.Dtos
{
	public class CreateGroupDto
	{ 
		public string Name { get; set; }
		public List<GropPermissionAddDto> GropPermissions { get; set; }
	}
}
