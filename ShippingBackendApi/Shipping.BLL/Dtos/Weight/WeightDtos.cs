using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.BLL.Dtos
{
    public record AddWeightDto
    {

        [Required(ErrorMessage = "DefaultWeight is required.")]
        public double DefaultWeight { get; set; }

        [Required(ErrorMessage = "AdditionalPrice is required.")]
        public double AdditionalPrice { get; set; }
    }
    public record UpdateWeightDtos
    {
        [Required(ErrorMessage = "Id is required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "DefaultWeight is required.")]
        public double DefaultWeight { get; set; }
        
        [Required(ErrorMessage = "AdditionalPrice is required.")]
        public double AdditionalPrice { get; set; }
    }
}
