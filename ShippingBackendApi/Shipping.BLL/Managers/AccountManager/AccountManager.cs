
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shipping.BLL.Dtos;
using Shipping.DAL.Data.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Shipping.BLL.Managers
{


    public class AccountManager:IAccountManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        public AccountManager(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;

        }

        public async Task<TokenDataDto> LoginUser(LoginDtos loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
        
            
            if (user == null || user.IsDeleted == true)
            {
                return null;
            }

            var result = await _userManager.CheckPasswordAsync(user, loginDTO.Password);

            if (!result)
            {
                return null;
            }
            string token = await GenerateToken(user);

            
            if (user is Employee) {
                Employee emp = (Employee)user;
                return new TokenDataDto
                {
                    tokenData = token,
                    name = emp.Name,
                    groupId = emp.GroupId
                };
            }

            else
            {
                return new TokenDataDto
                {
                    tokenData = token,
                    name = user.Name,
                    groupId = 0
                };

            }
              
        }

        public async Task LogoutUser()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<int> UniqeEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
                return 0;
            else
                return 1;
        }
        public async Task<int> UniqueUsername(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return user != null ? 0 : 1;
        }


        private async Task<string> GenerateToken(ApplicationUser user)
        {

            
            SecurityKey key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetSection("SecretKey").Value ?? string.Empty));
            
            SigningCredentials SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
           
            var claimsList = await _userManager.GetClaimsAsync(user);


            JwtSecurityToken token = new JwtSecurityToken(
                claims: claimsList,
                expires: DateTime.Now.AddDays(3),
                signingCredentials: SigningCredentials);


            var tokenString = new JwtSecurityTokenHandler().WriteToken(null);
            return tokenString;
        }

    }
}
