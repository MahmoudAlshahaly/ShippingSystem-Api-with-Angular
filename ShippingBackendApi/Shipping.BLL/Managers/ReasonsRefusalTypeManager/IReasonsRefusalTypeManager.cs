using Shipping.BLL.Dtos;
using Shipping.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.BLL
{
    public interface IReasonsRefusalTypeManager
    {
        Task<IEnumerable<ShowReasonsRefusalTypeDtos>> GetAll();
        Task<AddReasonsRefusalTypeDtos> GetById(int id);
        Task<int> Add(AddReasonsRefusalTypeDtos entity);
        Task<int> Update(UpdateReasonsRefusalTypeDtos entity);
        Task<int> Delete(int id);
    }
}
