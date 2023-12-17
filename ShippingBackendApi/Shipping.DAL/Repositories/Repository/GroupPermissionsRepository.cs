using Microsoft.EntityFrameworkCore;
using Shipping.DAL.Data;
using Shipping.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.DAL.Repositories
{
    public class GroupPermissionsRepository:TRepository<GroupPermission> ,IGroupPermissionsRepository
    {
        private readonly ShippingContext _context;

        public GroupPermissionsRepository(ShippingContext context) : base(context)
        {
            _context = context;
        }



        public async Task<List<GroupPermission>> GetGroupPermissionsByGroupId(int Id)
        {
            return _context.GroupPermissions.Where(gp => gp.GroupId == Id).Include(gp=>gp.Permission).ToList();
        }

        public async Task<int> AddRangeAsync(List<GroupPermission> groupPermissions)
        {
            if (groupPermissions == null || groupPermissions.Count == 0)
            {
                return 0;
            }

            await _context.GroupPermissions.AddRangeAsync(groupPermissions);
            return await SaveChangesAsync();
        }

        public async Task<int> RemoveRangeAsync(List<GroupPermission> groupPermissions)
        {
            if (groupPermissions == null || groupPermissions.Count == 0)
            {
                return 0;
            }

            _context.GroupPermissions.RemoveRange(groupPermissions);
            return await SaveChangesAsync();
        }
    }
}
