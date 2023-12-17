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
    public class Employee : ApplicationUser
    {
        [ForeignKey("Group")]
        public int GroupId { get; set; }
        public Group? Group { get; set; }

    }
}
