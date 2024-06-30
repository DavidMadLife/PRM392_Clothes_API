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
        Task<PRM392_ShopClothes_Repository.Entities.Member[]> SearchUser(string? keyword, int? pageNumber = 1, int? pageSize = 10);
        Task<PRM392_ShopClothes_Repository.Entities.Member?> GetUserById(long id);
        Task<PRM392_ShopClothes_Repository.Entities.Member?> GetUserByEmail(string email);
        Task<string> AuthorizeUser(LoginView loginView);
        Task<UpdateUserResponse> UpdateMember (int id, UpdateUserRequest updateUserRequest);
        Task<bool> DeleteUser(long id);
        Task<bool> ChangePassword(long id, string currentPassword, string newPassword);
    }
}
