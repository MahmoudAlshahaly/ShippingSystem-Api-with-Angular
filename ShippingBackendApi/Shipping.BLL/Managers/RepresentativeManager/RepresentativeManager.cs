using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shipping.BLL.Dtos;
using Shipping.BLL.Helpers;
using Shipping.DAL.Data.Models;
using Shipping.DAL.Params;
using Shipping.DAL.Repositories;
using Shipping.DAL.Specifications;
using System.Security.Claims;

namespace Shipping.BLL.Managers
{


    public class RepresentativeManager : IRepresentativeManager
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepository<Representative> _representativeRepo;
        private readonly IRepresentativeGovernatesRepository _representativeGovernatesRepository;


        public RepresentativeManager(UserManager<ApplicationUser> userManager, IRepository<Representative> representativeRepo, IRepresentativeGovernatesRepository representativeGovernatesRepository)
        {
            _userManager = userManager;
            _representativeRepo = representativeRepo;
            _representativeGovernatesRepository = representativeGovernatesRepository;

        }

        public async Task<int> UpdateRepresentativePassword(UpdatePasswordDtos updatePassDto)
        {

            var representative = await _representativeRepo.GetByCriteriaAsync(e => e.Id == updatePassDto.Id && !e.IsDeleted);
            if (representative == null)
            {
                return 0;
            }
            representative.Email = updatePassDto.Email;
            representative.PasswordHash = _userManager.PasswordHasher.HashPassword(representative, updatePassDto.Password);

            return await _representativeRepo.UpdateAsync(representative);
        }

        public async Task<int> UpdateRepresentative(RepresentativeUpdateDto updateDto)
        {
            var representative = await _representativeRepo.GetByCriteriaAsync(r => r.Id == updateDto.Id && !r.IsDeleted);

            if (representative == null)
            {
                return 0;
            }

            representative.Name = updateDto.Name;
            representative.PhoneNumber = updateDto.PhoneNumber;
            representative.BranchId = updateDto.BranchId;
            representative.Address = updateDto.Address;
            representative.Amount = updateDto.Amount;
            representative.Type = updateDto.Type;

            var representativeId = updateDto.Id;

            IQueryable<RepresentativeGovernate> existingGovernatesQuery = await _representativeGovernatesRepository.GetRepresentativeGovernatesByRepresentativeId(representativeId);
            List<RepresentativeGovernate> existingGovernates = await existingGovernatesQuery.ToListAsync();

            await _representativeGovernatesRepository.RemoveRangeAsync(existingGovernates);


            var representativeGovernates = updateDto.RepresentativeGovernates
                 .Select(g => new RepresentativeGovernate { RepresentativeId = representativeId, GovernorateId = g.GovernateId })
                 .ToList();

            await _representativeGovernatesRepository.AddRangeAsync(representativeGovernates);

            return await _representativeRepo.UpdateAsync(representative);

        }

        public async Task<getRepresentativeForUpdateDto> GetRepresentativeById(string RepresentativeId)
        {
            var representative = await _representativeRepo.GetByCriteriaAsync(r => r.Id == RepresentativeId && !r.IsDeleted);

            if (representative == null)
            {
                return null;
            }

            List<GovernatesDto> representativeGovernates = await _representativeGovernatesRepository
                 .GetRepresentativeGovernatesByRepresentativeId(representative.Id)
                 .Result
                 .Select(rg => new GovernatesDto { GovernateId = rg.GovernorateId, GovernateName = rg.Governorate.Name })
                 .ToListAsync();

            var dto = new getRepresentativeForUpdateDto
            {
                Name = representative.Name,
                PhoneNumber = representative.PhoneNumber,
                BranchId = representative.BranchId,
                Address = representative.Address,
                Amount = (double)representative.Amount,
                Type = (AmountType)representative.Type,
                RepresentativeGovernates = representativeGovernates
            };

            return dto;
        }
        public async Task<Pagination<GetAllRepresentativesDto>> GetAllRepresentativesAsync(GSpecParams representativeSpecParams)
        {

            var spec = new RepresentativeWithBranchSpecificaction(representativeSpecParams);
            var countSpec = new RepresentativeCountSpecification(representativeSpecParams);
            var totalItems = await _representativeRepo.CountAsync(countSpec);
            IReadOnlyList<Representative> representatives = await _representativeRepo.GetAllDataAsync(spec);
            var data = representatives.Select(e => new GetAllRepresentativesDto
            {
                Id = e.Id,
                Name = e.Name,
                Email = e.Email,
                Phone = e.PhoneNumber,
                Amount = e.Amount,
                Type = e.Type,
                BranchName = e.branch?.Name,
                IsDeleted = e.IsDeleted,
            }).ToList();

            return new Pagination<GetAllRepresentativesDto>(representativeSpecParams.PageIndex, representativeSpecParams.PageSize, totalItems, data);

        }
        public async Task<int> RegisterRepresentative(RepresentativeRegisterDto registrationDTO)
        {

            ApplicationUser Representative = new Representative
            {
                Name = registrationDTO.Name,
                UserName = registrationDTO.UserName,
                Email = registrationDTO.Email,
                PhoneNumber = registrationDTO.PhoneNumber,
                BranchId = registrationDTO.BranchId,
                Address = registrationDTO.Address,
                Amount = registrationDTO.Amount,
                Type = registrationDTO.Type

            };

            var result = await _userManager.CreateAsync(Representative, registrationDTO.Password);

            if (!result.Succeeded)
            {

                return 0;
            }



            var representativeId = Representative.Id;
            var representativeGovernates = registrationDTO.representativeGovernates
                .Select(g => new RepresentativeGovernate { RepresentativeId = representativeId, GovernorateId = g.GovernateId })
                .ToList();

            await _representativeGovernatesRepository.AddRangeAsync(representativeGovernates);


            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, Representative.Id),
                new Claim(ClaimTypes.Email, Representative.Email),
                 new Claim(ClaimTypes.Name, Representative.Name),
                new Claim(ClaimTypes.Role,"Representative")

            };

            await _userManager.AddClaimsAsync(Representative, claims);
            return 1;

        }
        public async Task<int> DeleteUser(string userId)
        {
            var representative = await _representativeRepo.GetByCriteriaAsync(r => r.Id == userId && !r.IsDeleted);

            if (representative == null)
                return 0;


            IQueryable<RepresentativeGovernate> existingGovernatesQuery = await _representativeGovernatesRepository.GetRepresentativeGovernatesByRepresentativeId(representative.Id);
            List<RepresentativeGovernate> existingGovernates = await existingGovernatesQuery.ToListAsync();

            await _representativeGovernatesRepository.RemoveRangeAsync(existingGovernates);

            representative.IsDeleted = true;
            return await _representativeRepo.UpdateAsync(representative);

        }



    }
}
