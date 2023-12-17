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
    public interface IMerchantManager
    {
        Task<int> RegisterMerchant(MerchantRegisterDto registrationDTO);
        Task<int> UpdateMerchantPassword(UpdatePasswordDtos updatePassDto);
        Task<int> UpdateMerchant(MerchantUpdateDto updateDto);
        Task<int> DeleteMerchant(string userId);
        Task<getMerchantForUpdateDto> GetMerchantByIdWithSpecialPrices(string merchantId);
        Task<Pagination<GetAllMerchantsDto>> GetAllMarchentsAsync(GSpecParams merchantSpecParams);

    }
}
