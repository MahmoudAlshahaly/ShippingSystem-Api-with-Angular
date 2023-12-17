using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shipping.API.Filters;
using Shipping.BLL.Dtos;
using Shipping.BLL.Managers;
using Shipping.DAL.Params;

namespace Shipping.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepresentativeController : ControllerBase
    {
        private readonly IRepresentativeManager _representativeManager;

        public RepresentativeController(IRepresentativeManager representativeManager)
        {
            _representativeManager = representativeManager;
        }


        [HttpPost]
        [TypeFilter(typeof(GpAttribute))]
        public async Task<IActionResult> RegisterRepresentative([FromBody] RepresentativeRegisterDto registrationDTO)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _representativeManager.RegisterRepresentative(registrationDTO);
            if (result > 0)
            {
                return Ok();
            }
            return StatusCode(500);
         
            
        }

      
        [HttpGet]
        [TypeFilter(typeof(GpAttribute))]
        public async Task<IActionResult> GetAllRepresentatives([FromQuery] GSpecParams representativeSpecParams)
        {
            var representatives = await _representativeManager.GetAllRepresentativesAsync(representativeSpecParams);
            return Ok(representatives);
        }

        [HttpPut]
        [TypeFilter(typeof(GpAttribute))]
        public async Task<IActionResult> UpdateRepresentative(string id, [FromBody] RepresentativeUpdateDto updateDto)
        {
            if (id != updateDto.Id)
                return BadRequest();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _representativeManager.UpdateRepresentative(updateDto);
             if (result > 0)
                return Ok();
            return StatusCode(500);
        }

       

        [HttpDelete]
        [TypeFilter(typeof(GpAttribute))]
        public async Task<IActionResult> DeleteRepresentative(string id)
        {
            var result = await _representativeManager.DeleteUser(id);
            if (result > 0)
            {
                return Ok();
            }
            return StatusCode(500);
        }

        [HttpPut("pass")]
        public async Task<IActionResult> UpdateRepresentativePass(string id, UpdatePasswordDtos updateDto)
        {
            if (id != updateDto.Id)
                return BadRequest();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _representativeManager.UpdateRepresentativePassword(updateDto);
            if (result > 0)
                return Ok();
            return StatusCode(500);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetRepresentativeById(string Id)
        {
            var representative = await _representativeManager.GetRepresentativeById(Id);
            if (representative == null)
                return NotFound();

            return Ok(representative);
        }
    }
}
