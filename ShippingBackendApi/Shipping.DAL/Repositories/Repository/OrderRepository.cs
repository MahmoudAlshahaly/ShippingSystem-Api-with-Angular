using Microsoft.EntityFrameworkCore;
using Shipping.DAL.Data;
using Shipping.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.DAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ShippingContext _context;

        public OrderRepository(ShippingContext context)
        {
            this._context = context;
        }
        public bool Add(Order entity)
        {
            try
            {
                _context.Orders.Add(entity);

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool Delete(Order order)
        {
            try
            {
                _context.Remove(order);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public Order GetById(int orderId)
        {
            try
            {
                var order = _context.Orders
                    .Include(gov => gov.Governorate)
                    .Include(city => city.City)
                    .Include(product=>product.Products)
                    .Include(ship=>ship.ShippingType)
                    .Include(branch=>branch.Branch)
                    .FirstOrDefault(o => o.Id == orderId);
                if (order != null)
                {
                    return order;
                }
                return null!;
            }
            catch (Exception)
            {
                return null!;
            }

        }

        //order reports
        public IEnumerable<Order> GetAll(int pageNumer, int pageSize)
        {
            return _context.Orders
                .Where(d => d.isDeleted == false)
                .Skip((pageNumer - 1) * pageSize)
                .Take(pageSize)
                .Include(gover => gover.Governorate)
                .Include(city => city.City)
                .Include(merchant => merchant.Merchant)
                .AsNoTracking();
        }
        public int CountAll()
        {
            return _context.Orders
                .Where(d => d.isDeleted == false)
                .Count();
        }
        public IEnumerable<Order> SearchByDateAndStatus(int pageNumer, int pageSize, DateTime fromDate, DateTime toDate, OrderStatus status)
        {
            toDate = toDate.AddDays(1);
            return _context.Orders
               .Where(d => d.isDeleted == false && d.Date > fromDate && d.Date < toDate && d.orderStatus == status)
               .Skip((pageNumer - 1) * pageSize)
               .Take(pageSize)
               .Include(gover => gover.Governorate)
               .Include(city => city.City)
               .Include(merchant => merchant.Merchant)
               .AsNoTracking();
        }
        public int CountOrdersByDateAndStatus(DateTime fromDate, DateTime toDate, OrderStatus status)
        {
            toDate = toDate.AddDays(1);
            return _context.Orders
               .Where(d => d.isDeleted == false && d.Date > fromDate && d.Date < toDate && d.orderStatus == status)
               .Count();
        }
       
        //home
        public List<int> CountOrdersForEmployeeByStatus()
        {
            return _context.Orders.Where(s => s.isDeleted == false).Select(s => (int)s.orderStatus).ToList();
        }
        public List<int> CountOrdersForMerchantByStatus(string merchantId)
        {
            return _context.Orders.Where(s => s.isDeleted == false && s.MerchantId == merchantId).Select(s => (int)s.orderStatus).ToList();
        }
        public List<int> CountOrdersForRepresentativeByStatus(string representativeId)
        {
            return _context.Orders.Where(s => s.isDeleted == false && s.RepresentativeId == representativeId).Select(s => (int)s.orderStatus).ToList();
        }

        //show orders
        public IEnumerable<Order> GetOrdersForEmployee(string searchText, int statusId, int pageNumer, int pageSize)
        {
            return _context.Orders
                .Where(o => o.orderStatus == (OrderStatus)statusId && o.isDeleted == false && o.ClientName.StartsWith(searchText))
                .Skip((pageNumer - 1) * pageSize)
                .Take(pageSize)
                .Include(gover => gover.Governorate)
                .Include(city => city.City)
                .AsNoTracking();
        }
        public IEnumerable<Order> GetOrdersForMerchant(string searchText, string merchantId, int statusId, int pageNumer, int pageSize)
        {
            return _context.Orders
                .Where(o => o.orderStatus == (OrderStatus)statusId && o.MerchantId == merchantId && o.isDeleted == false && o.ClientName.StartsWith(searchText))
                .Skip((pageNumer - 1) * pageSize)
                .Take(pageSize)
                .Include(gover => gover.Governorate)
                .Include(city => city.City)
                .AsNoTracking();
        }
        public IEnumerable<Order> GetOrdersForRepresentative(string representativeId, int statusId, int pageNumer, int pageSize, string searchText)
        {
            return _context.Orders
                .Where(o => o.orderStatus == (OrderStatus)statusId && o.isDeleted == false && o.RepresentativeId == representativeId && o.ClientName.StartsWith(searchText))
                .Skip((pageNumer - 1) * pageSize)
                .Take(pageSize)
                .Include(gover => gover.Governorate)
                .Include(city => city.City)
                .AsNoTracking();
        }

        //count for pagination
        public int GetCountOrdersForEmployee(int statusId, string searchText)
        {
            return _context.Orders
                .Where(o => o.orderStatus == (OrderStatus)statusId && o.isDeleted == false && o.ClientName.StartsWith(searchText))
                .Count();
        }
        public int GetCountOrdersForMerchant(string merchantId, int statusId, string searchText)
        {
            return _context.Orders
                .Where(o => o.orderStatus == (OrderStatus)statusId && o.MerchantId == merchantId && o.isDeleted == false && o.ClientName.StartsWith(searchText))
                .Count();
        }
        public int GetCountOrdersForRepresentative(string representativeId, int statusId, string searchText)
        {
            return _context.Orders
               .Where(o => o.orderStatus == (OrderStatus)statusId && o.isDeleted == false && o.RepresentativeId == representativeId && o.ClientName.StartsWith(searchText))
               .Count();
        }

        public bool SaveChanges()
        {
            try
            {
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

    }
}
