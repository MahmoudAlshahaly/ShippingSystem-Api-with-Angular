using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shipping.BLL.Dtos;
using Shipping.BLL;
using Shipping.DAL.Data.Models;
using Shipping.BLL.Managers;
using Shipping.API.Filters;

namespace Shipping.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliverToVillageController : ControllerBase
    {
        private readonly IDeliverToVillageManager _deliverToVillageManager;

        public DeliverToVillageController(IDeliverToVillageManager deliverToVillageManager)
        {
            this._deliverToVillageManager = deliverToVillageManager;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<DeliverToVillage>> GetDeliverToVillageById(int id)
        {
            var result = await _deliverToVillageManager.GetDeliverToVillageByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
       
      

        [HttpPut]
        [TypeFilter(typeof(GpAttribute))]
        public async Task<ActionResult> Update(DeliverToVillage d)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _deliverToVillageManager.Update(d);
            if (result > 0)
            {
                return Ok(new { message = "Deliver To Village Cost was updated successfully." });
            }
            ModelState.AddModelError("save", "Can't save Deliver To Village Cost may be something wrong!");
            return BadRequest(ModelState);
        }
    }
}
