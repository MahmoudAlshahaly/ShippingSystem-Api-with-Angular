using Shipping.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.BLL.Dtos
{
    public class RepresentativeRegisterDto:BaseRegisterDto

    { 
        //Goverments 

        [Required(ErrorMessage = "Amount is required.")]
        [Display(Name = "Amount")]
        public double Amount { get; set; }

        [Required(ErrorMessage = "Type is required.")]
        [Display(Name = "Type")]
        public AmountType Type { get; set; }

        public ICollection<RepresentativeGovernateDto> representativeGovernates { get; set; }



    }
}
