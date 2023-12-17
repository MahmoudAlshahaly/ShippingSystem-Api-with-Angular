using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.BLL.Dtos
{

    public class EmployeeRegisterDto:BaseRegisterDto
    {
       public int GroupId { get; set; }

    }
}
