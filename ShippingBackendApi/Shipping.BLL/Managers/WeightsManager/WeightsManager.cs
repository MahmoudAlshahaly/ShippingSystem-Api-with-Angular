using Shipping.BLL.Dtos;
using Shipping.DAL;
using Shipping.DAL.Data.Models;
using Shipping.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.BLL
{
    public class WeightsManager : IWeightsManager
    {
        private readonly IRepository<Weight> _weightsRepository;

        public WeightsManager(IRepository<Weight> weightsRepository)
        {
            _weightsRepository = weightsRepository;
        }

        public async Task<int> Add(AddWeightDto wightDto)
        {
            Weight weight = new Weight()
            {
                AdditionalPrice = wightDto.AdditionalPrice,
                DefaultWeight = wightDto.DefaultWeight

            };
            return await _weightsRepository.AddAsync(weight);
        }

        public async Task<Weight> GetWeightByIdAsync(int id)
        {
            var weight = await _weightsRepository.GetByIdAsync(id);
            if (weight == null)
            {
                return null;
            }
            else
            {
                return weight;
            }
        }

        public async Task<int> Update(UpdateWeightDtos wightDto)
        {
            var weight = await _weightsRepository.GetByCriteriaAsync(r => r.Id == wightDto.Id);

            if (weight != null)
            {
                weight.AdditionalPrice = wightDto.AdditionalPrice;
                weight.DefaultWeight = wightDto.DefaultWeight;

                return await _weightsRepository.UpdateAsync(weight);

            }
            return 0;
        }
    }
}
