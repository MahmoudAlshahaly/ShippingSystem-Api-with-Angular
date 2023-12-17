using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shipping.BLL.Dtos;
using Shipping.BLL;
using System.Threading.Tasks;
using Shipping.API.Filters;
using Microsoft.AspNetCore.Authorization;

namespace Shipping.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReasonsRefusalTypeController : ControllerBase
    {
        private readonly IReasonsRefusalTypeManager _reasonsTypeManager;

        public ReasonsRefusalTypeController(IReasonsRefusalTypeManager refusalTypeManager)
        {
            this._reasonsTypeManager = refusalTypeManager;
        }


        [HttpPost]
        [TypeFilter(typeof(GpAttribute))]
        public async Task<ActionResult> Add(AddReasonsRefusalTypeDtos order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _reasonsTypeManager.Add(order);
            if (result>0)
            {
                return Ok(new { message = "ReasonsRefusalType was added successfully." });
            }
            ModelState.AddModelError("save", "Can't save ReasonsRefusalType may be something wrong!");
            return BadRequest(ModelState);
        }


        [HttpPut]
        [TypeFilter(typeof(GpAttribute))]
        public async Task<ActionResult> Update(UpdateReasonsRefusalTypeDtos order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result =await _reasonsTypeManager.Update(order);
            if (result>0)
            {
                return Ok(new { message = "ReasonsRefusalType was updated successfully." });
            }
            ModelState.AddModelError("save", "Can't save may be something wrong!");
            return BadRequest(ModelState);
        }


        [HttpDelete]
        [TypeFilter(typeof(GpAttribute))]
        public async Task<ActionResult> Delete(int ReasonsRefusalTypeId)
        {
            var result =await _reasonsTypeManager.Delete(ReasonsRefusalTypeId);
            if (result>0)
            {
                return Ok(new { message = "ReasonsRefusalType was deleted successfully." });
            }
            return BadRequest("ReasonsRefusalType used in products delete them first.");
        }


        [HttpGet]
        [Route("GetAllForDropDown")]
        [Authorize(Policy = "RepresentativeOnly")]
        public async Task<ActionResult<IEnumerable<ShowReasonsRefusalTypeDtos>>> GetAllForGropDown()
        {
            var reasons = await _reasonsTypeManager.GetAll();
            return Ok(reasons);
        }



        [HttpGet]
        [Route("GetAll")]
        [TypeFilter(typeof(GpAttribute))]
       
        public async Task<ActionResult<IEnumerable<ShowReasonsRefusalTypeDtos>>> GetAll()
        {
            var reasons = await _reasonsTypeManager.GetAll();
            return Ok(reasons);
        }


        [HttpGet]
        [Route("GetById")]
        public async Task<ActionResult<IEnumerable<AddReasonsRefusalTypeDtos>>> GetById(int ReasonsRefusalTypeId)
        {
            var ReasonsRefusalType = await _reasonsTypeManager.GetById(ReasonsRefusalTypeId);
            if (ReasonsRefusalType!=null)
            {
                return Ok(ReasonsRefusalType);
            }
            return BadRequest(new { message = "Item not found" });
        }
    }
}
