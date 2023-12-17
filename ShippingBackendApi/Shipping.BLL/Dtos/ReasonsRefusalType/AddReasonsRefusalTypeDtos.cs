using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.BLL.Dtos
{
    public record AddReasonsRefusalTypeDtos
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = string.Empty;
    }

    public record ShowReasonsRefusalTypeDtos
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool isDeleted { get; set; } 
    }
  

    public record UpdateReasonsRefusalTypeDtos
    {
        [Required(ErrorMessage = "Id is required.")]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = string.Empty;
    }

}
