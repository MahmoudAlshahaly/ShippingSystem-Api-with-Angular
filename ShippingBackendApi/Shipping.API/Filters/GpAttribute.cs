using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Shipping.BLL.Managers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Shipping.DAL.Data.Models;
using Shipping.BLL.Dtos;

namespace Shipping.API.Filters
{
    public class GpAttribute: ActionFilterAttribute
    {    public string Property1 { get; set; }
        private readonly ClaimsPrincipal claimsPrincipal;
        private readonly IConfiguration _configuration;
        private readonly IGroupPermissionManager _groupPermissionManager ;
        private readonly IEmployeeManager _employeeManager;
        public GpAttribute(IConfiguration configuration,
        IGroupPermissionManager groupPermissionManager,
        IEmployeeManager employeeManager)
        {
            _configuration = configuration;
            _groupPermissionManager = groupPermissionManager;
            _employeeManager = employeeManager;
        }

        public override  async void OnActionExecuting(ActionExecutingContext context)
        {
            var actionName = context.HttpContext.Request.Method.ToLower();
            var controllerName = context.Controller.GetType().Name.ToLower();
            string operation = string.Empty;
            //var token = getToken(context);
            var groupId =await  GetGroupId(context.HttpContext.User.Identities.First().Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).ElementAt(0));
            bool isValid = true;
            List<GroupPermissionDto> groupPermissions = new List<GroupPermissionDto>();
            if (groupId != 0)
            {
                
                var permissions =await _groupPermissionManager.HasPermission(groupId);
                if (permissions != null)
                {
                    groupPermissions = permissions.Where(p => controllerName.Contains(p.Name.ToLower())).ToList();
                }
            }

            if (groupPermissions != null)
            {
                switch (actionName)
                {
                    case "post":
                        isValid = groupPermissions.FirstOrDefault(gp => gp.Action == "Add") == null ? false : true;


                        break;
                    case "get":
                        isValid = groupPermissions.FirstOrDefault(gp => gp.Action == "Show") == null ? false : true;

                        break;
                    case "put":
                        isValid = groupPermissions.FirstOrDefault(gp => gp.Action == "Edit") == null ? false : true;

                        break;
                    case "delete":
                        isValid = groupPermissions.FirstOrDefault(gp => gp.Action == "Delete") == null ? false : true;
                        break;
                }
            }
            else
            {
                isValid = false;
            }
            if (isValid == false)
            {
                context.ModelState.AddModelError(controllerName, "employee not authorized ");
                context.Result = new BadRequestObjectResult(context.ModelState);
            }

        }
        async Task<int> GetGroupId(string id)
        {
          var emp =await _employeeManager.GetEmployeeById(id);

            return emp.GroupId;
        }
        //int  GetGroupId(string token)
        //{
        //    var secret = _configuration["SecretKey"] ?? string.Empty;
        //    var key = Encoding.ASCII.GetBytes(secret);
        //    var handler = new JwtSecurityTokenHandler();
        //    var validations = new TokenValidationParameters
        //    {
        //        ValidateIssuerSigningKey = true,
        //        IssuerSigningKey = new SymmetricSecurityKey(key),
        //        ValidateIssuer = false,
        //        ValidateAudience = false
        //    };
        //    var empId = handler.ValidateToken(token, validations, out var tokenSecure).Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).ElementAt(0);
        //    var emp =   _employeeManager.GetEmployeeById(empId).Result;

        //    return emp.GroupId;
        //}
        string getToken(ActionExecutingContext context)
        {
            return context.HttpContext.Request.Headers["Authorization"][0].Split(" ")[1];
        }
    }
}
