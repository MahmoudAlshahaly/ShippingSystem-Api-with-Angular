using Microsoft.AspNetCore.Mvc;
using Shipping.API.Filters;
using Shipping.BLL.Dtos;
using Shipping.BLL.Managers;
using Shipping.DAL.Data.Models;
using Shipping.DAL.Params;

namespace Shipping.API.Controllers
{
    [Route("api/[controller]")]
    [TypeFilter(typeof(IpBlockActionFilterAttribute))]

    [ApiController]
    //[RequireHttps]

    public class AccountController : ControllerBase
    {

        private readonly IAccountManager _userManager;
        private readonly ILogger<AccountController> logger;

        public AccountController(IAccountManager userManager,ILogger<AccountController> logger)
        {
            _userManager = userManager;
            this.logger = logger;
        }
      
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDtos loginDTO)
        {
            try
            {
                var token = await _userManager.LoginUser(loginDTO);
                if (token == null)
                {
                    logger.LogError("finish action");
                    return Ok(new { message = "Invalid" });
                }
                else
                    logger.LogError("finish action");

                return Ok(new { Token = token });
            }
            catch(Exception ex) 
            {
                logger.LogError("finish action");
                return BadRequest(ex.Message);
            }
        }



        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _userManager.LogoutUser();
                return Ok(new { message = "Logged out successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("email")]
        public async Task<IActionResult> UniqueEmail( string email)
        {

            var result =  await _userManager.UniqeEmail(email);
               
            if(result == 0)
            return Ok(new { message = "Invalid" });

            else if(result==1)    
            return Ok(new { message = "Valid" });
            

            return BadRequest();

        }

        [HttpPost("userName")]
        public async Task<IActionResult> UniqueUserName(string userName)
        {

            var result = await _userManager.UniqueUsername(userName);

            if (result == 0)
                return Ok(new { message = "Invalid" });
            else if (result == 1)
            {
                return Ok(new { message = "Valid" });
            }

            return BadRequest();

        }


    }




}

