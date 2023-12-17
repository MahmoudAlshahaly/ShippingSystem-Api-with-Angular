using Microsoft.AspNetCore.Identity;
using Shipping.BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.BLL.Managers
{
    public interface IAccountManager
    {
        Task<TokenDataDto> LoginUser(LoginDtos loginDTO);

        Task LogoutUser();
        Task<int> UniqeEmail(string email);


       Task<int> UniqueUsername(string username);


    }
}
