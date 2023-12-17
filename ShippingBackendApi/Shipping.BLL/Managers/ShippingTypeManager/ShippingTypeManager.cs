using Microsoft.EntityFrameworkCore;
using Shipping.BLL;
using Shipping.BLL.Dtos;
using Shipping.DAL.Data.Models;
using Shipping.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.BLL
{
    public class ShippingTypeManager : IShippingTypeManager
    {
       
        private readonly IRepository<ShippingType> _shippingTypeRepository;

        public ShippingTypeManager(IRepository<ShippingType> shippingTypeRepository)
        {
            _shippingTypeRepository = shippingTypeRepository;
        }

        public async Task<ShowShippingTypeDto> GetShippingTypeByIdAsync(int id)
        {
            var shippingType = await _shippingTypeRepository.GetByIdAsync(id);
            if (shippingType == null)
            {
                return null;
            }

            return new ShowShippingTypeDto
            {
                Id = shippingType.Id,
                Name = shippingType.Name,
                Cost=shippingType.Cost,
                IsDeleted = shippingType.IsDeleted,
                Status=shippingType.status
            };
        }

        public async Task<IEnumerable<GetAllTypesDto>> GetAllShippingTypeAsync()
        {
            var result  = await _shippingTypeRepository.GetAllAsync();
            var shippingTypes = result.Where(s => !s.IsDeleted && s.status);

            return shippingTypes.Select(s => new GetAllTypesDto
            {
                id = s.Id,
                name = s.Name,
            });
        }

        public async Task<IEnumerable<ShowShippingTypeDto>> GetAllShippingTypeWithDeletedAsync()
        {
            var shippingTypes = await _shippingTypeRepository.GetAllAsync();

            return shippingTypes.Where(s=>s.IsDeleted==false)
                .Select(s => new ShowShippingTypeDto
                {
                    Id=s.Id,
                    Name = s.Name,
                    Cost = s.Cost,
                    IsDeleted = s.IsDeleted,
                    Status=s.status
                });
        }

        public async Task<int> CreateShippingTypeAsync(AddShippingTypeDto shippingTypeDto)
        {
            var shippingType = new ShippingType
            {
                Name = shippingTypeDto.Name,
                Cost = shippingTypeDto.Cost
            };

            return await _shippingTypeRepository.AddAsync(shippingType);
        }

        public async Task<int> UpdateShippingTypeAsync(UpdatShippingTypeDto shippingTypeDto)
        {
            var shippingType = await _shippingTypeRepository.GetByIdAsync(shippingTypeDto.Id);
            if (shippingType == null)
            {
                return 0;
            }

            shippingType.Name = shippingTypeDto.Name;
            shippingType.Cost = shippingTypeDto.Cost;

            return await _shippingTypeRepository.UpdateAsync(shippingType);
        }

        public async Task<int> DeleteShippingTypeAsync(int id)
        {
            var shippingType = await _shippingTypeRepository.GetByIdAsync(id);
            if (shippingType == null)
            {
                return 0;
            }

            shippingType.IsDeleted = true;

            return await _shippingTypeRepository.UpdateAsync(shippingType);
        }

        public async Task<int> ToggleShippingTypeStatusAsync(int id)
        {
            var shippingType = await _shippingTypeRepository.GetByIdAsync(id);
            if (shippingType == null)
            {
                return 0;
            }

            shippingType.status = !shippingType.status;

            return await _shippingTypeRepository.UpdateAsync(shippingType);
        }
    }
}
