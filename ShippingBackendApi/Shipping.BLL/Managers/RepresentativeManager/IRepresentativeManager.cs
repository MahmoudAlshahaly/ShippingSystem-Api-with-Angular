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
    public interface IRepresentativeManager
    {
        Task<int> UpdateRepresentativePassword(UpdatePasswordDtos updatePassDto);
        Task<int> UpdateRepresentative(RepresentativeUpdateDto updateDto);
        Task<getRepresentativeForUpdateDto> GetRepresentativeById(string RepresentativeId);
        Task<Pagination<GetAllRepresentativesDto>> GetAllRepresentativesAsync(GSpecParams representativeSpecParams);
        Task<int> RegisterRepresentative(RepresentativeRegisterDto registrationDTO);
        Task<int> DeleteUser(string userId);

    }
}
