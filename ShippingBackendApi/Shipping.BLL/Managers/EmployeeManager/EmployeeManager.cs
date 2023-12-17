using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shipping.BLL.Dtos;
using Shipping.BLL.Helpers;
using Shipping.DAL.Data.Models;
using Shipping.DAL.Params;
using Shipping.DAL.Repositories;
using Shipping.DAL.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.BLL.Managers
{
    public class EmployeeManager:IEmployeeManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepository<Employee> _employeeRepo;
        public EmployeeManager( UserManager<ApplicationUser> userManager,IRepository<Employee> employeeRepo) {

            _userManager = userManager;
            _employeeRepo = employeeRepo;

        }

        public async Task<int> DeleteEmployee(string employeeId)
        {

            if (employeeId == "4372156e-b7ef-4fc3-aa82-9f50742f127d")
                return -1;

            var employee = await _employeeRepo.GetByCriteriaAsync(e => e.Id == employeeId && !e.IsDeleted);
            if (employee == null)
            {
                 return 0;
            }
            employee.IsDeleted = true;
      
            return await _employeeRepo.UpdateAsync(employee);
        
        }
       
        public async Task<int> UpdateEmployee(EmployeeUpdateDto updateDto)
        {
            var employee = await _employeeRepo.GetByCriteriaAsync(e => e.Id == updateDto.Id && !e.IsDeleted);
            if (employee == null)
            {
                return 0;
            }
            employee.Name = updateDto.Name;
            employee.PhoneNumber = updateDto.PhoneNumber;
            employee.Address = updateDto.Address;
            employee.GroupId = updateDto.GroupId;
            employee.BranchId = updateDto.BranchId;

           

            return await _employeeRepo.UpdateAsync(employee);
        }

        public async Task<int> UpdateEmployeePassword(UpdatePasswordDtos updatePassDto)
        {
            
            var employee = await _employeeRepo.GetByCriteriaAsync(e => e.Id == updatePassDto.Id && !e.IsDeleted);
            if (employee == null)
            {
                return 0;
            }
            employee.Email = updatePassDto.Email;
            employee.PasswordHash = _userManager.PasswordHasher.HashPassword(employee, updatePassDto.Password);
        
            return await _employeeRepo.UpdateAsync(employee);
        }

        public async Task<int> RegisterEmployee(EmployeeRegisterDto registrationDTO)
        {
            ApplicationUser employee = new Employee
            {
                Name = registrationDTO.Name,
                UserName = registrationDTO.UserName,
                Email = registrationDTO.Email,
                PhoneNumber = registrationDTO.PhoneNumber,
                BranchId = registrationDTO.BranchId,
                Address = registrationDTO.Address,
                GroupId = registrationDTO.GroupId,
            };

            var result = await _userManager.CreateAsync(employee, registrationDTO.Password);

            if (!result.Succeeded)
            {
                return 0;
            }
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, employee.Id),
                new Claim(ClaimTypes.Email, employee.Email),
                new Claim(ClaimTypes.Name, employee.Name),
                new Claim(ClaimTypes.Role,"Employee")
            };

            await _userManager.AddClaimsAsync(employee, claims);
            return 1;

        }
        public async Task<getEmployeeForUpdateDtos> GetEmployeeById(string employeeId)
        {
            var employee = await _employeeRepo.GetByCriteriaAsync(e => e.Id == employeeId && !e.IsDeleted);

            if (employee == null)
            {
                return null;
            }

            var dto = new getEmployeeForUpdateDtos
            {
                Id = employee.Id,
                Name = employee.Name,
                PhoneNumber = employee.PhoneNumber,
                BranchId = employee.BranchId,
                Address = employee.Address,
                GroupId = employee.GroupId,
            };

            return dto;
        }
        public async Task<Pagination<GetAllEmployeesDto>> GetAllEmployeesAsync(GSpecParams employeeSpecParams)
        {

            var spec = new EmployeeWithBranchAndGroupSpecificaction(employeeSpecParams);
            var countSpec = new EmployeeCountSpecification(employeeSpecParams);
            var totalItems = await _employeeRepo.CountAsync(countSpec);
            IReadOnlyList<Employee> employees = await _employeeRepo.GetAllDataAsync(spec);
            var data = employees.Select(e => new GetAllEmployeesDto
            {
                Id = e.Id,
                Name = e.Name,
                Email = e.Email,
                Phone = e.PhoneNumber,
                IsDeleted = e.IsDeleted,
                BranchName = e.branch.Name,
                GroupName = e.Group.Name
            }).ToList();
            return new Pagination<GetAllEmployeesDto>(employeeSpecParams.PageIndex, employeeSpecParams.PageSize, totalItems, data);
        }

    }
}
