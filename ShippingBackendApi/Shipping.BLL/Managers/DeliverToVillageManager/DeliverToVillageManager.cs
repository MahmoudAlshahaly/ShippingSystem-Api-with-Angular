using Shipping.BLL.Dtos;
using Shipping.DAL.Data.Models;
using Shipping.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.BLL.Managers
{
    public class DeliverToVillageManager : IDeliverToVillageManager
    {
        private readonly IRepository<DeliverToVillage> _deliverToVillageRepository;

        public DeliverToVillageManager(IRepository<DeliverToVillage> deliverToVillageRepository)
        {
            _deliverToVillageRepository = deliverToVillageRepository;
        }

        public async Task<int> Add(DeliverToVillage d)
        {
            DeliverToVillage newDeliverToVillage = new DeliverToVillage()
            {
                AdditionalCost = d.AdditionalCost

            };
            return await _deliverToVillageRepository.AddAsync(newDeliverToVillage);
        }

        public async Task<DeliverToVillage> GetDeliverToVillageByIdAsync(int id)
        {
            var result = await _deliverToVillageRepository.GetByIdAsync(id);
            if (result == null)
            {
                return null;
            }
            else
            {
                return result;
            }
        }

        public async Task<int> Update(DeliverToVillage d)
        {
            var result = await _deliverToVillageRepository.GetByCriteriaAsync(r => r.Id == d.Id);

            if (result != null)
            {
                result.AdditionalCost = d.AdditionalCost;

                return await _deliverToVillageRepository.UpdateAsync(result);

            }
            return 0;
        }
    }
}
