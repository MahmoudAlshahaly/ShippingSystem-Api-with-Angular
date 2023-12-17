using Shipping.BLL.Dtos;
using Shipping.BLL.Helpers;
using Shipping.DAL.Data.Models;
using Shipping.DAL.Params;
using Shipping.DAL.Repositories;
using Shipping.DAL.Specifications;

namespace Shipping.BLL.Managers
{

    public class GovernorateManager : IGovernorateManager
    {

        private readonly IRepository<Governorate> _governorateRepository;

        public GovernorateManager(IRepository<Governorate> governorateRepository)
        {
            _governorateRepository = governorateRepository;
        }

      
        public async Task<IEnumerable<ShowGovernorateWithCityDto>> GetAllGovernorateWithCityAsync()
        {
            var result =await _governorateRepository.GetAllAsync();
            var nonDeletedGovernorates =result.Where(g => g.IsDeleted==false);

            return nonDeletedGovernorates.Select(g => new ShowGovernorateWithCityDto
            {
                Id = g.Id,
                Name = g.Name,
           
                Cities = g.Cities.Where(c => c.isDeleted==false).Select(c => new ShowCityForDropdownDto
                {
                    Id = c.Id,
                    Name = c.Name,


                }).ToList()
            });
        }

        public async Task<IEnumerable<UpdateGovernorateDto>> GetAllGovernorateAsync()
        {
            var result =await _governorateRepository.GetAllAsync();
              var nonDeletedGovernorates =  result.Where(g => !g.IsDeleted);

            return nonDeletedGovernorates.Select(g => new UpdateGovernorateDto
            {
                Id = g.Id,
                Name = g.Name,


            });
        }

        public async Task<Pagination<ShowGovernorateDto>> GetAllGovernorateWithDeletedAsync(GSpecParams govSpecParams)
        {

              var spec = new GovernorateSpecificaction(govSpecParams);
            var countSpec = new GovernorateSpecificaction(govSpecParams);
            var totalItems = await _governorateRepository.CountAsync(countSpec);
            IReadOnlyList<Governorate> governorates = await _governorateRepository.GetAllDataAsync(spec);
            var data = governorates.Select(g => new ShowGovernorateDto
            {
                Id = g.Id,
                Name = g.Name,
                IsDeleted = g.IsDeleted,
            }).ToList();

            return new Pagination<ShowGovernorateDto>(govSpecParams.PageIndex, govSpecParams.PageSize, totalItems, data);
        }

        public async Task<int> CreateGovernorateAsync(AddGovernorateDto governorateDto)
        {

            return await _governorateRepository.AddAsync(new Governorate { Name = governorateDto.Name });
        }

        public async Task<int> UpdateGovernorateAsync(UpdateGovernorateDto governorateDto)
        {
            Governorate governorate = await _governorateRepository.GetByIdAsync(governorateDto.Id);
            if (governorate == null)
            {
                return 0;
            }

            governorate.Name = governorateDto.Name;

            return await _governorateRepository.UpdateAsync(governorate);


        }


 
        public async Task<int> DeleteGovernorateAsync(int id)
        {
            var governorate = await _governorateRepository.GetByCriteriaAsync(g=>g.Id==id && !g.IsDeleted , new[] { "Cities" } );
            var count = governorate.Cities.Count();
            if (governorate == null)
            {
                return 0;
            }

            if (count > 0)
                return -1;
            governorate.IsDeleted = true;

            return await _governorateRepository.UpdateAsync(governorate);
        }
        
        
    }
}



