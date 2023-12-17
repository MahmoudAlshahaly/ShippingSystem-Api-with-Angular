using Shipping.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.BLL.Dtos
{
    public class MerchantRegisterDto:BaseRegisterDto
    {
        public int CityId { get; set; }
        public int GovernorateId { get; set; }
        [Required(ErrorMessage = "StoreName is required.")]
        public string StoreName { get; set; } = string.Empty;


        [Required(ErrorMessage = "PickUp is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "PickUp must be a positive number.")]
        public double PickUp { get; set; }


        [Required(ErrorMessage = "ReturnerPercent is required.")]
        [Range(0, 100, ErrorMessage = "ReturnerPercent must be a percentage between 0 and 100.")]
        public double ReturnerPercent { get; set; }


        public List<SpecialPriceDto> SpecialPrices { get; set; }

    }
}




