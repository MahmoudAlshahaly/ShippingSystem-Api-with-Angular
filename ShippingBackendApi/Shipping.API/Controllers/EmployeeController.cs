using Microsoft.AspNetCore.Mvc;
using Shipping.API.Filters;
using Shipping.BLL.Dtos;
using Shipping.BLL.Managers;
using Shipping.DAL.Params;

namespace Shipping.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeManager _employeeManager;

        public EmployeeController(IEmployeeManager employeeManager)
        {
            _employeeManager = employeeManager;
        }

        [HttpPost]
       [TypeFilter(typeof(GpAttribute))]
        public async Task<IActionResult> RegisterEmployee(EmployeeRegisterDto registrationDto)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _employeeManager.RegisterEmployee(registrationDto);
            if (result > 0)
            {
                return Ok();
            }
            return StatusCode(500);

        }


        [HttpDelete]
        [TypeFilter(typeof(GpAttribute))]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            var result = await _employeeManager.DeleteEmployee(id);

            if (result == -1)
                return Ok(new { Message = "Admin Can not be deleted" });
            if (result > 0)
            {
                return Ok();
            }
            
            return StatusCode(500);
        }

        [HttpPut]
        [TypeFilter(typeof(GpAttribute))]
        public async Task<IActionResult> UpdateEmployee(string id, EmployeeUpdateDto updateDto)
        {

            if (id != updateDto.Id)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var result = await _employeeManager.UpdateEmployee(updateDto);
            if (result > 0)
            {
                return Ok();
            }

            return StatusCode(500);



        }


        [HttpGet]
        [TypeFilter(typeof(GpAttribute))]
        public async Task<IActionResult> GetAllEmployees([FromQuery] GSpecParams employeeSpecParams)
        {

            var employees = await _employeeManager.GetAllEmployeesAsync(employeeSpecParams);
            return Ok(employees);


        }



        [HttpPut("pass")]
        public async Task<IActionResult> UpdateEmployeePass(string id, UpdatePasswordDtos updateDto)
        {

            if (id != updateDto.Id)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var result = await _employeeManager.UpdateEmployeePassword(updateDto);
            if (result > 0)
            {
                return Ok();
            }

            return StatusCode(500);

        }





        [HttpGet("{Id}")]
        public async Task<IActionResult> GetEmployeeById(string Id)
        {
            var employee = await _employeeManager.GetEmployeeById(Id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }



    }
}
    
