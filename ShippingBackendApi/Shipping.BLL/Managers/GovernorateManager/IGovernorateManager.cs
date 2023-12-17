using Shipping.BLL.Dtos;
using Shipping.BLL.Helpers;
using Shipping.DAL.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.BLL.Managers
{
    public interface IGovernorateManager
    {

       
        Task<IEnumerable<ShowGovernorateWithCityDto>> GetAllGovernorateWithCityAsync();
        Task<Pagination<ShowGovernorateDto>> GetAllGovernorateWithDeletedAsync(GSpecParams govSpecParams);
        Task<IEnumerable<UpdateGovernorateDto>> GetAllGovernorateAsync();
        Task<int> CreateGovernorateAsync(AddGovernorateDto GovernorateDto);
        Task<int> UpdateGovernorateAsync(UpdateGovernorateDto GovernorateDto);
        Task<int> DeleteGovernorateAsync(int id);
        

    }
}
