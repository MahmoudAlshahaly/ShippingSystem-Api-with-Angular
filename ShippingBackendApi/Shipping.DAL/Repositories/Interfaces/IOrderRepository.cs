using Shipping.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.DAL.Repositories
{
    public interface IOrderRepository
    {
        bool Add(Order entity);
        bool Delete(Order entity);
        Order GetById(int id);

        //order reports
        IEnumerable<Order> GetAll(int pageNumer, int pageSize);
        int CountAll();
        IEnumerable<Order> SearchByDateAndStatus(int pageNumer, int pageSize,DateTime fromDate, DateTime toDate,OrderStatus status);
        int CountOrdersByDateAndStatus(DateTime fromDate, DateTime toDate, OrderStatus status);
       
        //home page
        List<int> CountOrdersForEmployeeByStatus();
        List<int> CountOrdersForMerchantByStatus(string merchantId);
        List<int> CountOrdersForRepresentativeByStatus(string representativeId);

        //show orders
        IEnumerable<Order> GetOrdersForEmployee(string searchText, int statusId, int pageNumer, int pageSize);
        IEnumerable<Order> GetOrdersForMerchant(string searchText, string merchantId,int statusId, int pageNumer, int pageSize);
        IEnumerable<Order> GetOrdersForRepresentative(string representativeId, int statusId, int pageNubmer, int pageSize, string searchText);
        
        //count for pagination
        int GetCountOrdersForEmployee(int statusId, string searchText);
        int GetCountOrdersForMerchant(string merchantId, int statusId, string searchText);
        int GetCountOrdersForRepresentative(string representativeId, int statusId, string searchText);
        bool SaveChanges();

    }
}
