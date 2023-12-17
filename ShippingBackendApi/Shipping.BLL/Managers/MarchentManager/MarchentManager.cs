using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shipping.BLL.Dtos;
using Shipping.BLL.Helpers;
using Shipping.DAL.Data.Models;
using Shipping.DAL.Params;
using Shipping.DAL.Repositories;
using Shipping.DAL.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.BLL.Managers
{
    public class MarchentManager : IMerchantManager
    {

        private readonly IRepository<Merchant> _merchantRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISpecialPricesRepository  _specialPricesRepository;
        public MarchentManager(UserManager<ApplicationUser> userManager, IRepository<Merchant> merchantRepo, ISpecialPricesRepository specialPricesRepository)
        {
            _userManager = userManager;
            _merchantRepo = merchantRepo;
            _specialPricesRepository = specialPricesRepository;
        }




        public async Task<int> RegisterMerchant(MerchantRegisterDto registrationDTO)
        {
            List<SpecialPrice> specialPrices = registrationDTO.SpecialPrices.Select(p => new SpecialPrice
            {
                Price = p.Price,
                CityId = p.CityId,
                GovernorateId = p.GovernorateId,
                MerchentId = null
            }).ToList();

            ApplicationUser Merchant = new Merchant
            {
                Name = registrationDTO.Name,
                UserName = registrationDTO.UserName,
                Email = registrationDTO.Email,
                PhoneNumber = registrationDTO.PhoneNumber,
                BranchId = registrationDTO.BranchId,
                Address = registrationDTO.Address,
                StoreName = registrationDTO.StoreName,
                PickUp = registrationDTO.PickUp,
                ReturnerPercent = registrationDTO.ReturnerPercent,
                CityId = registrationDTO.CityId,
                GovernorateId = registrationDTO.GovernorateId,
                SpecialPrices = specialPrices


            };

            var result = await _userManager.CreateAsync(Merchant, registrationDTO.Password);
            if (!result.Succeeded)
            {

                return 0;
            }

            foreach (var specialPrice in specialPrices)
            {
                specialPrice.MerchentId = Merchant.Id;
            }

            await _specialPricesRepository.AddRangeAsync(specialPrices);
            

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, Merchant.Id),  
                new Claim(ClaimTypes.Email, Merchant.Email),
                 new Claim(ClaimTypes.Name, Merchant.Name),
                new Claim(ClaimTypes.Role,  "Merchant")

            };
            await _userManager.AddClaimsAsync(Merchant, claims);
            return 1;
        }


        public async Task<int> UpdateMerchantPassword(UpdatePasswordDtos updatePassDto)
        {

            var merchant = await _merchantRepo.GetByCriteriaAsync(e => e.Id == updatePassDto.Id && !e.IsDeleted);
            if (merchant == null)
            {
                return 0;
            }
            merchant.Email = updatePassDto.Email;
            merchant.PasswordHash = _userManager.PasswordHasher.HashPassword(merchant, updatePassDto.Password);

            return await _merchantRepo.UpdateAsync(merchant);
        }



        public async Task<int> UpdateMerchant(MerchantUpdateDto updateDto)
        {
            var merchant = await _merchantRepo.GetByCriteriaAsync(m => m.Id == updateDto.Id && !m.IsDeleted );

            if (merchant == null)
            {

                return 0;

            }

            merchant.Name = updateDto.Name;
            merchant.PhoneNumber = updateDto.PhoneNumber;
            merchant.BranchId = updateDto.BranchId;
            merchant.Address = updateDto.Address;
            merchant.StoreName = updateDto.StoreName;
            merchant.PickUp = updateDto.PickUp;
            merchant.ReturnerPercent = updateDto.ReturnerPercent;
            merchant.CityId = updateDto.CityId;
            merchant.GovernorateId = updateDto.GovernorateId;

            var specialPrices = updateDto.SpecialPrices.Select(p => new SpecialPrice
            {
                Price = p.Price,
                CityId = p.CityId,
                GovernorateId = p.GovernorateId,
                MerchentId = updateDto.Id
            }).ToList();

            List<SpecialPrice> existingSpecialPrices =await _specialPricesRepository.GetSpecialPricesByMerchantId(updateDto.Id);
            await _specialPricesRepository.RemoveRangeAsync(existingSpecialPrices);
            await _specialPricesRepository.AddRangeAsync(specialPrices);
            var result = await _merchantRepo.UpdateAsync(merchant);

            return 1;
         }
        public async Task<int> DeleteMerchant(string Id)
        {


            var merchant = await _merchantRepo.GetByCriteriaAsync(m => m.Id == Id && !m.IsDeleted);
            if (merchant == null)
            {
                return 0;
            }
            List<SpecialPrice> existingSpecialPrices =await _specialPricesRepository.GetSpecialPricesByMerchantId(merchant.Id);
            await _specialPricesRepository.RemoveRangeAsync(existingSpecialPrices);
            merchant.IsDeleted = true;

            return await _merchantRepo.UpdateAsync(merchant);
        }
        public async Task<getMerchantForUpdateDto> GetMerchantByIdWithSpecialPrices(string merchantId)
        {
            var merchant = await _merchantRepo.GetByCriteriaAsync(m => m.Id == merchantId && !m.IsDeleted);


            if (merchant == null)
            {
                return null;
            }
            List<SpecialPrice> specialPrices = await _specialPricesRepository.GetSpecialPricesByMerchantId(merchantId);
               

            var merchantDto = new getMerchantForUpdateDto
            {
                Name = merchant.Name,
                PhoneNumber = merchant.PhoneNumber,
                Address = merchant.Address,
                StoreName = merchant.StoreName,
                PickUp = merchant.PickUp,
                ReturnerPercent = (double)merchant.ReturnerPercent,
                BranchId = merchant.BranchId,
                CityId = (int)merchant.CityId,
                GovernorateId = (int)merchant.GovernorateId,
                SpecialPrices = specialPrices.Select(sp => new SpecialPriceDto
                {
                    Price = sp.Price,
                    GovernorateId = sp.GovernorateId,
                    CityId = sp.CityId
                }).ToList()
            };

            return merchantDto;
        }

        public async Task<Pagination<GetAllMerchantsDto>> GetAllMarchentsAsync(GSpecParams merchantSpecParams)
        {
            var spec = new MerchantWithGovernorateAndBranchSpecificaction(merchantSpecParams);
            var countSpec = new MerchantCountSpecification(merchantSpecParams);
            var totalItems = await _merchantRepo.CountAsync(countSpec);
            IReadOnlyList<Merchant> merchants = await _merchantRepo.GetAllDataAsync(spec);
            var data = merchants.Select(m => new GetAllMerchantsDto
            {
                Id = m.Id,
                Name = m.Name,
                Email = m.Email,
                Phone = m.PhoneNumber,
                ReturnerPercent = m.ReturnerPercent,
                StoreName = m.StoreName,
                GovernateName = m.Governorate.Name,
                IsDeleted = m.IsDeleted,
                BranchName = m.branch.Name,
            }).ToList();
            return new Pagination<GetAllMerchantsDto>(merchantSpecParams.PageIndex, merchantSpecParams.PageSize, totalItems, data);
        }
       
    }
}
