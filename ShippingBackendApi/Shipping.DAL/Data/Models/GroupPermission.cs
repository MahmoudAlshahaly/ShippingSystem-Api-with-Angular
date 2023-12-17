using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.DAL.Data.Models
{
	public class GroupPermission
	{
		public int id { get; set; }
		public string Action { get; set; } 
		public int GroupId { get; set; }

		public Group? Group { get; set; }
		public int PermissionId { get; set; }
		public Permission? Permission { get; set; }	

	}
}
