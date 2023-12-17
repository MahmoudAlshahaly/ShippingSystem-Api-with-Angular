using Shipping.BLL.Dtos;
using Shipping.BLL.Helpers;
using Shipping.DAL.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.BLL.Managers
{
    public interface IEmployeeManager
    {
   
            Task<int> DeleteEmployee(string userId);
            Task<int> UpdateEmployee(EmployeeUpdateDto updateDto);
        Task<int> UpdateEmployeePassword(UpdatePasswordDtos updatePassDto);
            Task<int> RegisterEmployee(EmployeeRegisterDto registrationDTO);
            Task<getEmployeeForUpdateDtos> GetEmployeeById(string employeeId);
            Task<Pagination<GetAllEmployeesDto>> GetAllEmployeesAsync(GSpecParams employeeSpecParams);
        }
    }

