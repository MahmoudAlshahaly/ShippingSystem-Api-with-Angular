using Shipping.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.BLL.Dtos
{
    public class SpecialPriceDto
    {
        public decimal Price { get; set; }
        public int GovernorateId { get; set; }
        public int CityId { get; set; }

    }
}
