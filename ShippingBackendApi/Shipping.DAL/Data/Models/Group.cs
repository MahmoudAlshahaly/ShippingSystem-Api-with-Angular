using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.DAL.Data.Models
{
	public class Group
	{
		public int Id { get; set; }
		public string Name { get; set; }=string.Empty;
        public DateTime Date { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;

		public ICollection<GroupPermission> GroupPermissions =new HashSet<GroupPermission>();
		public ICollection<Employee> Employees = new HashSet<Employee>();
	}
}
