using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Execution;
using PRM392_ShopClothes_Model.Model.Request;
using PRM392_ShopClothes_Model.Model.Response;


namespace PRM392_ShopClothes_Service.Interface
{
    public interface IUserService
    {
        Task<RegisterUserResponse> RegisterUser(RegisterUserRequest registerUserRequest);
        Task<Member[]> SearchUser(string keyword, int pageNumber = 1, int pageSize = 10);
        Task<Member?> GetUserById(long id);
        Task<Member?> GetUserByEmail(string email);
        Task<string> AuthorizeUser(LoginView loginView);
    }
}
