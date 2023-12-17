using Microsoft.EntityFrameworkCore;
using Shipping.BLL.Dtos;
using Shipping.DAL;
using Shipping.DAL.Data;
using Shipping.DAL.Data.Models;
using Shipping.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.BLL
{
    public class ReasonsRefusalTypeManager : IReasonsRefusalTypeManager
    {
        private readonly IRepository<ReasonsRefusalType> _reasonsRepository;

        public ReasonsRefusalTypeManager(IRepository<ReasonsRefusalType> reasonsRepository)
        {
            _reasonsRepository = reasonsRepository;
        }

        public async Task<int> Add(AddReasonsRefusalTypeDtos entity)
        {
            ReasonsRefusalType reasonsRefusalType = new ReasonsRefusalType()
            {
                Name = entity.Name,
                isDeleted = false,
            };

            return await _reasonsRepository.AddAsync(reasonsRefusalType);
        }
        public async Task<int> Update(UpdateReasonsRefusalTypeDtos entity)
        {
            var reasonsRefusalType = await _reasonsRepository.GetByCriteriaAsync(r => r.Id == entity.Id && r.isDeleted == false);

            if (reasonsRefusalType != null)
            {
                reasonsRefusalType.Name = entity.Name;

                return await _reasonsRepository.UpdateAsync(reasonsRefusalType);

            }
            return 0;
        }
        public async Task<int> Delete(int reasonsRefusalTypeId)
        {
            var reasonsRefusalType = await _reasonsRepository.GetByCriteriaAsync(r => r.Id == reasonsRefusalTypeId && r.isDeleted == false);

            if (reasonsRefusalType != null)
            {
                reasonsRefusalType.isDeleted = true;

                return await _reasonsRepository.UpdateAsync(reasonsRefusalType);

            }
            return 0;
        }
        public async Task<IEnumerable<ShowReasonsRefusalTypeDtos>> GetAll()
        {

            var result = await _reasonsRepository.GetAllAsync();

            var nonDeletedReasons = result.Where(b => !b.isDeleted);
            return nonDeletedReasons.Select(s => new ShowReasonsRefusalTypeDtos
            { Id =s.Id,
                Name = s.Name,
                isDeleted = s.isDeleted
            }).ToList();

        }

        public async Task<AddReasonsRefusalTypeDtos> GetById(int id)
        {
            var reasonsRefusalType = await _reasonsRepository.GetByCriteriaAsync(r => r.Id == id && r.isDeleted == false);

            if (reasonsRefusalType != null)
            {
                AddReasonsRefusalTypeDtos result = new AddReasonsRefusalTypeDtos()
                {
                    Name = reasonsRefusalType.Name
                };
                return result;
            }

            return null;
        }
    }
}
