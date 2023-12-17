using Microsoft.AspNetCore.Mvc;
using Shipping.API.Filters;
using Shipping.BLL.Dtos;
using Shipping.BLL.Managers;
using Shipping.DAL.Params;

namespace Shipping.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MerchantController : ControllerBase
    {

        private readonly IMerchantManager _merchantManager;

        public MerchantController(IMerchantManager merchantManager)
        {
            _merchantManager = merchantManager;
        }

        [HttpPost]
        [TypeFilter(typeof(GpAttribute))]
        public async Task<IActionResult> RegisterMerchant(MerchantRegisterDto registrationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _merchantManager.RegisterMerchant(registrationDto);

            if (result > 0)
            {
                return Ok();
            }
            return StatusCode(500);

        }


        [HttpPut]
        [TypeFilter(typeof(GpAttribute))]
        public async Task<IActionResult> UpdateMerchant(string id, MerchantUpdateDto updateDto)
        {

            if (id != updateDto.Id)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _merchantManager.UpdateMerchant(updateDto);

            if (result > 0)
            {
                return Ok();
            }

            return StatusCode(500);
        }

      
        [HttpDelete]
        [TypeFilter(typeof(GpAttribute))]
        public async Task<IActionResult> DeleteMerchant(string id)
        {
            var result = await _merchantManager.DeleteMerchant(id);

            if (result > 0)
            {
                return Ok();
            }
            return StatusCode(500);
        }

       

        [HttpGet]
        [TypeFilter(typeof(GpAttribute))]
        public async Task<IActionResult> GetAllMerchants([FromQuery] GSpecParams merchantSpecParams)
        {
            var merchants = await _merchantManager.GetAllMarchentsAsync(merchantSpecParams);
            return Ok(merchants);
        }



        [HttpPut("pass")]
        public async Task<IActionResult> UpdateMerchantPass(string id, UpdatePasswordDtos updateDto)
        {

            if (id != updateDto.Id)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _merchantManager.UpdateMerchantPassword(updateDto);

            if (result > 0)
            {
                return Ok();
            }

            return StatusCode(500);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetMerchantById(string id)
        {
            var merchant = await _merchantManager.GetMerchantByIdWithSpecialPrices(id);

            if (merchant == null)
            {
                return NotFound();
            }

            return Ok(merchant);
        }
    }
}

