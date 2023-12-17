using Shipping.DAL.Data;
using Shipping.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShippingContext _context;

        public ProductRepository(ShippingContext context)
        {
            this._context = context;
        }

        public bool AddRange(List<Product> entity)
        {
            try
            {
                _context.Products.AddRange(entity);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteRange(List<Product> products)
        {
            try
            {
                _context.Products.RemoveRange(products);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Product> GetProductsByOrderId( int id)
        {
           return _context.Products.Where(d => d.isDeleted == false && d.OrderId == id).ToList();
        }
    }
}
