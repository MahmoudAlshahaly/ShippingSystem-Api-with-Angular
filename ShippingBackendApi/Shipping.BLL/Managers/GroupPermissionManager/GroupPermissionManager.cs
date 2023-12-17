using Shipping.BLL.Dtos;
using Shipping.DAL.Data.Models;
using Shipping.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.BLL.Managers
{
    public class GroupPermissionManager : IGroupPermissionManager
    {
        private readonly IGroupPermissionsRepository _groupPermissionRepo;
        public GroupPermissionManager(IGroupPermissionsRepository groupPermissionRepo)
        {
            _groupPermissionRepo = groupPermissionRepo;
            
        }

        public async Task<IEnumerable<GroupPermissionDto>> HasPermission( int groupId)
        {

            var data = await _groupPermissionRepo.GetGroupPermissionsByGroupId(groupId);
            return data.Select(d => new GroupPermissionDto
            {
                Name = d.Permission.Name,
                Action = d.Action
            });

        }


    }
}
