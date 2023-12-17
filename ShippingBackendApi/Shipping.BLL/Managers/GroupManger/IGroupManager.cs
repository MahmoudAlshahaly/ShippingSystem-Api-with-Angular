using Shipping.BLL.Dtos;
using Shipping.BLL.Helpers;
using Shipping.DAL.Data.Models;
using Shipping.DAL.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.BLL.Managers
{
    public interface IGroupManager
    {
        Task<int> AddGroupAsync(CreateGroupDto createGroupDto);
        Task<IEnumerable<GroupForDropDown>> GetAllAsync();
        Task<int> DeleteGroupAsync(int id);
        Task<GroupWithPermissionsDto> GetGroupByIdWithPermissionsAsync(int id);
        Task<int> UpdateGroupAsync( UpdateGroupDto groupDto);
        Task<Pagination<GroupDto>> GetAllAsync(GSpecParams groupSpecParams);

    }
}
