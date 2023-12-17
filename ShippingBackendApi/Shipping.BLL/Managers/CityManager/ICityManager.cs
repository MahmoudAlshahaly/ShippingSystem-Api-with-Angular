using Shipping.BLL.Dtos;
using Shipping.BLL.Dtos.CityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.BLL.Managers
{
    public interface ICityManager
    {

        Task<IEnumerable<ShowCityDto>> GetAllAsync(int govId);
        Task<int> CreateCityAsync(AddCityDto   cityDto);
        Task<int> UpdateCityAsync(UpdateCityDto cityDto);
        Task<int> DeleteCityAsync(int id);
        Task<UpdateCityDto> GetCityAsync(int id);
    }
}
