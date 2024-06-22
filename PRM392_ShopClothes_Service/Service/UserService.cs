using AutoMapper;
using AutoMapper.Execution;
using PRM392_ShopClothes_Model.Model.Request;
using PRM392_ShopClothes_Model.Model.Response;
using PRM392_ShopClothes_Repository.Repository;
using PRM392_ShopClothes_Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392_ShopClothes_Service.Service
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        
        private readonly IUnitOfWork _unitOfWork;


        public UserService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        public Task<string> AuthorizeUser(LoginView loginView)
        {
            throw new NotImplementedException();
        }

        public Task<Member?> GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<Member?> GetUserById(long id)
        {
            throw new NotImplementedException();
        }

        public Task<RegisterUserResponse> RegisterUser(RegisterUserRequest registerUserRequest)
        {
            var existingMember = _unitOfWork.MemberRepository.Get(filter: v => v.Email == registerUserRequest.Email).FirstOrDefault();
            if (existingMember != null) { 
            
                throw new NotImplementedException();
            }

            var member = _mapper.Map<Member>(registerUserRequest);
            member.
        }

        public Task<Member[]> SearchUser(string keyword, int pageNumber = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }
    }
}
