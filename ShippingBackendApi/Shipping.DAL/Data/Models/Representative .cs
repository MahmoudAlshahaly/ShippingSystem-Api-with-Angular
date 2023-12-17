using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.DAL.Data.Models
{
    public enum AmountType
    {
        Percent,
        Fixed
    }

    public class Representative: ApplicationUser
    {
      
        public double? Amount { get; set; }
        public AmountType? Type { get; set; }

        public ICollection<RepresentativeGovernate> RepresentativeGovernates = new HashSet<RepresentativeGovernate>();


    }
}
