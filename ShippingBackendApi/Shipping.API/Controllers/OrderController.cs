using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shipping.API.Filters;
using Shipping.BLL;
using Shipping.BLL.Dtos;
using Shipping.DAL.Data.Models;

namespace Shipping.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderManager _orderManager;

        public OrderController(IOrderManager orderManager)
        {
            this._orderManager = orderManager;
        }

        [HttpPost]
        [Authorize(Policy = "MerchantOnly")]
        public async Task<ActionResult<AddOrderResultDto>> Add(AddOrderDto order)
        {
            var result = await _orderManager.Add(order);
            if (result.IsSuccesfull && ModelState.IsValid)
            {
                return Ok(new { message = "Order was added successfully.",result });
            }
            ModelState.AddModelError("save", "Can't save Order may be some ID'S wrong!");
            return BadRequest(ModelState);
        }
              
        
        [HttpPut]
        [Authorize(Policy = "MerchantOnly")]
        public async Task<ActionResult<UpdateOrderResultDto>> Update(UpdateOrderDto order)
        {
            var result =await _orderManager.Update(order);
            if (result.IsSuccesfull && ModelState.IsValid)
            {
                return Ok(new { message = "Order was updated successfully.", result });
            }
            ModelState.AddModelError("save", "Can't save Order may be some ID'S wrong!");
            return BadRequest(ModelState);
        }

        [HttpDelete]
        [Authorize(Policy = "MerchantOnly")]
        public ActionResult Delete(int orderId)
        {
            var result = _orderManager.Delete(orderId);
            if (result)
            {
                return Ok(new { message = "Order was deleted successfully." });
            }
            return BadRequest("Order not found");
        }

        [HttpGet]
        [Route("Get/{id}")]
        public ActionResult<ReadOrderDto> GetById(int id)
        {
            var order = _orderManager.GetById(id);
            if (order != null)
            {
                return Ok(order);
            }
            return BadRequest(new { message = "Item not found" });
        }


        //Employee

        [HttpGet]
        [Route("CountOrdersForEmployeeByStatus")]
        public ActionResult CountOrdersForEmployeeByStatus()
        {
            return Ok(_orderManager.CountOrdersForEmployeeByStatus());
        }
        [HttpGet]
        [Route("GetOrdersForEmployee")]
        [TypeFilter(typeof(GpAttribute))]
        public ActionResult<IEnumerable<ReadOrderDto>> GetOrdersForEmployee(int statusId, int pageNubmer, int pageSize, string searchText = "")
        {
            return Ok(_orderManager.GetOrdersForEmployee(searchText, statusId, pageNubmer, pageSize));
        }

        [HttpGet]
        [Route("GetCountOrdersForEmployee")]
        [TypeFilter(typeof(GpAttribute))]
        public ActionResult<int> GetCountOrdersForEmployee(int statusId, string searchText = "")
        {
            return Ok(_orderManager.GetCountOrdersForEmployee(statusId, searchText));
        }
      
        [HttpPut]
        [Route("SelectRepresentative")]
        [TypeFilter(typeof(GpAttribute))]
        public ActionResult SelectRepresentative(int orderId, string representativeId)
        {
            bool result = _orderManager.SelectRepresentative(orderId, representativeId);
            if (result)
            {
                return Ok(new { message = "Selected Successfully" });

            }
            return BadRequest(new { message = "Item not found" });
        }


        [HttpGet("DropdownListRepresentative")]
        [TypeFilter(typeof(GpAttribute))]
        public async Task<IActionResult> DropdownListRepresentative(int orderId)
        {
            try
            {
                return Ok(await _orderManager.DropdownListRepresentativeAsync(orderId));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }


        //Merchant
        [HttpGet]
        [Route("CountOrdersForMerchantByStatus")]
        [Authorize(Policy = "MerchantOnly")]
        public ActionResult CountOrdersForMerchantByStatus(string id)
        {
            return Ok(_orderManager.CountOrdersForMerchantByStatus(id));
        }

        [HttpGet]
        [Route("GetOrdersForMerchant")]
        [Authorize(Policy = "MerchantOnly")]
        public ActionResult<IEnumerable<ReadOrderDto>> GetOrdersForMerchant(string merchantId, int statusId, int pageNubmer, int pageSize, string searchText = "")
        {
            return Ok(_orderManager.GetOrdersForMerchant(searchText, merchantId, statusId, pageNubmer, pageSize));
        }

        [HttpGet]
        [Route("GetCountOrdersForMerchant")]
        [Authorize(Policy = "MerchantOnly")]
        public ActionResult<int> GetCountOrdersForMerchant(string merchantId, int statusId, string searchText = "")
        {
            return Ok(_orderManager.GetCountOrdersForMerchant(merchantId, statusId, searchText));
        }


        //Employee and Merchant
        [HttpPut]
        [Route("ChangeStatus")]
        public ActionResult ChangeStatus(int orderId, OrderStatus status)
        {
            bool result = _orderManager.ChangeStatus(orderId, status);
            if (result)
            {
                return Ok(new { message = "Changed Successfully" });

            }
            return BadRequest(new { message = "Item not found" });
        }

        //Representative 
        [HttpGet]
        [Route("CountOrdersForRepresentativeByStatus")]
        [Authorize(Policy = "RepresentativeOnly")]
        public ActionResult CountOrdersForRepresentativeByStatus(string representativeId)
        {
            return Ok(_orderManager.CountOrdersForRepresentativeByStatus(representativeId));
        }
       
        [HttpGet]
        [Route("GetOrdersForRepresentative")]
        [Authorize(Policy = "RepresentativeOnly")]
        public ActionResult<IEnumerable<ReadOrderDto>> GetOrdersForRepresentative(string representativeId, int statusId, int pageNubmer, int pageSize, string searchText = "")
        {
            return Ok(_orderManager.GetOrdersForRepresentative(representativeId, statusId, pageNubmer, pageSize, searchText));
        }

        [HttpGet]
        [Route("GetCountOrdersForRepresentative")]
        [Authorize(Policy = "RepresentativeOnly")]
        public ActionResult<int> GetCountOrdersForRepresentative(string representativeId, int statusId, string searchText = "")
        {
            return Ok(_orderManager.GetCountOrdersForRepresentative(representativeId, statusId, searchText));
        }

        [HttpPut]
        [Route("ChangeStatusAndReasonRefusal")]
        [Authorize(Policy = "RepresentativeOnly")]
        public ActionResult ChangeStatusAndReasonRefusal(int orderId, OrderStatus status, int? reasonRefusal)
        {
            bool result = _orderManager.ChangeStatusAndReasonRefusal(orderId, status, reasonRefusal);
            if (result)
            {
                return Ok(new { message = "Changed Successfully" });

            }
            return BadRequest(new { message = "Item not found" });
        }

  
        //Display order
        [HttpGet]
        [Route("GetAllDataById")]
        public ActionResult<ReadAllOrderDataDto> GetAllDataById(int id)
        {
            var order = _orderManager.GetAllDataById(id);
            if (order != null)
            {
                return Ok(order);
            }
            return BadRequest(new { message = "Item not found" });
        }
    }
} 
