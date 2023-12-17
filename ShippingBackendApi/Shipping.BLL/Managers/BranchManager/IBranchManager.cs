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
    public interface IBranchManager
    {
       
            Task<getBranchByIdDto> GetBranchByIdAsync(int id);
            Task<Pagination<ShowBranchDto>> GetAllBranchesAsync(GSpecParams branchSpecParams);
            Task<IEnumerable<UpdateBranchDto>> GetAllAsync();
            Task<int> CreateBranchAsync(AddBranchDto branchDto);
            Task<int> UpdateBranchAsync(UpdateBranchDto branchDto);
            Task<int> DeleteBranchAsync(int id);
            Task<int> ChangeStatusBranchAsync(int id);



    }
}
