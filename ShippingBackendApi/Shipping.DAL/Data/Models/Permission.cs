using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.DAL.Data.Models
{
	public class Permission
	{
		public int Id { get; set; }
		public string Name { get; set; }=string.Empty;

		public ICollection<GroupPermission> GroupPermissions=new HashSet<GroupPermission>();
	} 
}
