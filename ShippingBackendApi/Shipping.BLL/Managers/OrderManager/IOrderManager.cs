using Shipping.BLL.Dtos;
using Shipping.BLL.Dtos.RepresentativeDtos;
using Shipping.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.BLL
{
    public interface IOrderManager
    {
        Task<AddOrderResultDto> Add(AddOrderDto order);
        Task<UpdateOrderResultDto> Update(UpdateOrderDto order);
        bool Delete(int order);
        UpdateOrderDto GetById(int orderId);
        ReadAllOrderDataDto GetAllDataById(int orderId);

        //order reports
        IEnumerable<ReadOrderReportsDto> GetAll(int pageNumer, int pageSize);
        IEnumerable<ReadOrderReportsDto> SearchByDateAndStatus(int pageNumer, int pageSize, DateTime fromDate, DateTime toDate, OrderStatus status);
        int CountAll();
        int CountOrdersByDateAndStatus(DateTime fromDate, DateTime toDate, OrderStatus status);
       
        //Employee
        List<int> CountOrdersForEmployeeByStatus();
        IEnumerable<ReadOrderDto> GetOrdersForEmployee(string searchText,int statusId, int pageNumer, int pageSize);
        int GetCountOrdersForEmployee(int statusId,string searchText);
        bool SelectRepresentative(int OrderId, string representativeId);
        Task<List<DropdownListRepresentativeDto>> DropdownListRepresentativeAsync(int orderId);

        //Merchant
        List<int> CountOrdersForMerchantByStatus(string merchantId);
        IEnumerable<ReadOrderDto> GetOrdersForMerchant(string searchText, string merchantId, int statusId, int pageNumer, int pageSize);
        int GetCountOrdersForMerchant(string merchantId, int statusId, string searchText);
        
        //Employee and Merchant
        bool ChangeStatus(int OrderId, OrderStatus status);


        //Representative
        List<int> CountOrdersForRepresentativeByStatus(string representativeId);
        IEnumerable<ReadOrderDto> GetOrdersForRepresentative(string representativeId, int statusId, int pageNumer, int pageSize, string searchText);
        int GetCountOrdersForRepresentative(string representativeId, int statusId, string searchText);
        bool ChangeStatusAndReasonRefusal(int OrderId, OrderStatus status, int? reasonRefusal);


        
    }
}
