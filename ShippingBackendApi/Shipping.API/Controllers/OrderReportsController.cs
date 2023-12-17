using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shipping.API.Filters;
using Shipping.BLL;
using Shipping.BLL.Dtos;
using Shipping.DAL.Data.Models;

namespace Shipping.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderReportsController : ControllerBase
    {
        private readonly IOrderManager _orderManager;

        public OrderReportsController(IOrderManager orderManager)
        {
            this._orderManager = orderManager;
        }


        [HttpGet]
        [Route("GetAllOrder")]
        [TypeFilter(typeof(GpAttribute))]
        public ActionResult<IEnumerable<ReadOrderReportsDto>> GetAllOrder(int pageNubmer, int pageSize)
        {
            return Ok(_orderManager.GetAll(pageNubmer, pageSize));
        }



        [HttpGet]
        [Route("CountAll")]
        [TypeFilter(typeof(GpAttribute))]
        public ActionResult<int> CountAll()
        {
            return Ok(_orderManager.CountAll());
        }
       

        [HttpGet]
        [Route("SearchByDateAndStatus")]
        [TypeFilter(typeof(GpAttribute))]
        public ActionResult<IEnumerable<ReadOrderReportsDto>> SearchByDateAndStatus(int pageNubmer, int pageSize, DateTime fromDate, DateTime toDate, OrderStatus status)
        {
            return Ok(_orderManager.SearchByDateAndStatus(pageNubmer, pageSize, fromDate, toDate, status));
        }
        

        [HttpGet]
        [Route("CountOrdersByDateAndStatus")]
        [TypeFilter(typeof(GpAttribute))]
        public ActionResult<int> CountOrdersByDateAndStatus(DateTime fromDate, DateTime toDate, OrderStatus status)
        {
            return Ok(_orderManager.CountOrdersByDateAndStatus(fromDate, toDate, status));
        }

       

    }
}
