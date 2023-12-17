using Shipping.DAL.Data.Models;
using Shipping.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.DAL.Repositories
{
    public interface IGroupPermissionsRepository
    {
        Task<List<GroupPermission>> GetGroupPermissionsByGroupId(int Id);
        Task<int> AddRangeAsync(List<GroupPermission> groupPermissions);
        Task<int> RemoveRangeAsync(List<GroupPermission> groupPermissions);

    }
}
