using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shipping.BLL.Dtos;
using Shipping.DAL.Data.Models;

namespace Shipping.BLL
{
    public interface IShippingTypeManager
    {
        Task<ShowShippingTypeDto> GetShippingTypeByIdAsync(int id);
        Task<IEnumerable<GetAllTypesDto>> GetAllShippingTypeAsync();
        Task<IEnumerable<ShowShippingTypeDto>> GetAllShippingTypeWithDeletedAsync();
        Task<int> CreateShippingTypeAsync(AddShippingTypeDto shippingTypeDto);
        Task<int> UpdateShippingTypeAsync(UpdatShippingTypeDto shippingTypeDto);
        Task<int> DeleteShippingTypeAsync(int id);
        Task<int> ToggleShippingTypeStatusAsync(int id);

    }
}
