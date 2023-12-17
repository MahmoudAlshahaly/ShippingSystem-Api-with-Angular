using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.DAL.Data.Models
{
   public class ApplicationUser: IdentityUser
    {
        [Required]
        public string Address { get; set; } = string.Empty;


        [Required]
        [ForeignKey("branch")]
        public int BranchId { get; set; } 
        public virtual Branch? branch { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;

    }
}
