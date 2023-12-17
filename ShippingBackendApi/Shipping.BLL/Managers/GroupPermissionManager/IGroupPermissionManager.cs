using Shipping.BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.BLL.Managers
{
    public interface IGroupPermissionManager
    {
        Task<IEnumerable<GroupPermissionDto>> HasPermission(int groupId);
    }
}
