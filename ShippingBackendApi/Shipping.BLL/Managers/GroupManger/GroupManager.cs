using Microsoft.EntityFrameworkCore;
using Shipping.BLL.Dtos;
using Shipping.BLL.Helpers;
using Shipping.DAL.Data.Models;
using Shipping.DAL.Params;
using Shipping.DAL.Repositories;
using Shipping.DAL.Specifications;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.BLL.Managers
{
    public class GroupManager : IGroupManager
    {
     
        private readonly IRepository<Group> _groupRepo;
        private readonly IGroupPermissionsRepository _groupPermissionRepo;
        public GroupManager(IRepository<Group> groupRepo ,IGroupPermissionsRepository groupPermissionRepo)
        {
          
            _groupRepo = groupRepo;
            _groupPermissionRepo=groupPermissionRepo;
        }

        
        public async Task<int> AddGroupAsync(CreateGroupDto createGroupDto)
        {
            Group group = new Group()
            {
                Name = createGroupDto.Name,
                GroupPermissions = createGroupDto.GropPermissions.Select(gp => new GroupPermission
                {
                    PermissionId = gp.PermissionId,
                    Action = gp.Action
                }).ToList() 

            };


           return await _groupRepo.AddAsync(group);

        }

        public async Task<int> DeleteGroupAsync(int id)
        {
           
            var group = await _groupRepo.GetByCriteriaAsync(g => g.Id == id && !g.IsDeleted, new[] { "Employees" });
            var count=group.Employees.Count();
         

            if (group == null )
                return 0;

            if (count > 0)
                return -1;

            group.IsDeleted = true;

           return  await _groupRepo.UpdateAsync(group);

        }
            
        public async Task<GroupWithPermissionsDto> GetGroupByIdWithPermissionsAsync(int id)
        {
            Group? group = await _groupRepo.GetAllAsync().Result
             .Include(group => group.GroupPermissions)
             .ThenInclude(gp => gp.Permission).FirstOrDefaultAsync(g => g.Id == id && !g.IsDeleted );

            if (group == null)
                return null;
            return new GroupWithPermissionsDto
            {
                Id = group.Id,
                Name = group.Name,
                Date=group.Date,
                Permissions = group.GroupPermissions.Select(gp => new PermissionDto
                {
                    Id = gp.Permission.Id,
                    Name = gp.Permission.Name,
                    Action = gp.Action             
                }).ToList()
            };

        }

        public async Task<int> UpdateGroupAsync(UpdateGroupDto groupDto)
        {
            var group = await _groupRepo.GetByCriteriaAsync(g => g.Id == groupDto.Id && !g.IsDeleted);
            if (group == null)
                return 0;

            group.Name = groupDto.Name;
            List<GroupPermission> existingGroupPermissions = await _groupPermissionRepo.GetGroupPermissionsByGroupId(group.Id);
        
            await _groupPermissionRepo.RemoveRangeAsync(existingGroupPermissions);
            await _groupPermissionRepo.AddRangeAsync(groupDto.groupPermissions.Select(gp => new GroupPermission
            {
                GroupId = groupDto.Id,
                PermissionId = gp.PermissionId,
                Action = gp.Action

            }).ToList()
            );

            return await _groupRepo.UpdateAsync(group);
        }
        public async Task<IEnumerable<GroupForDropDown>> GetAllAsync()
        {
            List<Group> groups = await _groupRepo.GetAllAsync().Result.Where(g => g.IsDeleted == false).ToListAsync();
            return groups.Select(g => new GroupForDropDown
            {
                Id = g.Id,
                Name = g.Name,
            }).ToList();
        }
        public async Task<Pagination<GroupDto>> GetAllAsync(GSpecParams groupSpecParams)
        {

            var spec = new GroupSpecificaction(groupSpecParams);
            var countSpec = new GroupCountSpecification(groupSpecParams);
            var totalItems = await _groupRepo.CountAsync(countSpec);
            IReadOnlyList<Group> groups = await _groupRepo.GetAllDataAsync(spec);
            var data = groups.Select(g => new GroupDto
            {
                Id = g.Id,
                Name=g.Name,
                Date=g.Date
            }).ToList();
      
            return new Pagination<GroupDto>(groupSpecParams.PageIndex, groupSpecParams.PageSize, totalItems, data);

        }

       
    }
}
