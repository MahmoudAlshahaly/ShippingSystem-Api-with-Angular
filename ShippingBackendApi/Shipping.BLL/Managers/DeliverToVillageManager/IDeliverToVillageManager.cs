using Shipping.BLL.Dtos;
using Shipping.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.BLL.Managers
{
    public interface IDeliverToVillageManager
    {
        Task<int> Add(DeliverToVillage d);
        Task<int> Update(DeliverToVillage d);
        Task<DeliverToVillage> GetDeliverToVillageByIdAsync(int id);
    }
}
