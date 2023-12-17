using Shipping.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.DAL.Repositories
{
    public interface IProductRepository
    {
        bool AddRange(List<Product> products);
        bool DeleteRange(List<Product> products);
        List<Product> GetProductsByOrderId(int id);
    }
}
