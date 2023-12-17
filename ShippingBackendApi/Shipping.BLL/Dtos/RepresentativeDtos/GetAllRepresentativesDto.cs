using Shipping.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.BLL.Dtos
{
    public class GetAllRepresentativesDto
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? BranchName { get; set; }
        public double? Amount { get; set; }
        public AmountType? Type { get; set; }
        public bool IsDeleted { get; set; }
    }
}
