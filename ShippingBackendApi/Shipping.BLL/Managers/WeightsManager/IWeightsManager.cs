using Shipping.BLL.Dtos;
using Shipping.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.BLL
{
    public interface IWeightsManager
    {
        Task<int> Add(AddWeightDto order);
        Task<int> Update(UpdateWeightDtos order);
        Task<Weight> GetWeightByIdAsync(int id);
    }
}
