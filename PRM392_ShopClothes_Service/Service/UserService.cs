using AutoMapper;
using AutoMapper.Execution;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PRM392_ShopClothes_Model.Model.Request;
using PRM392_ShopClothes_Model.Model.Response;
using PRM392_ShopClothes_Repository.Entities;
using PRM392_ShopClothes_Repository.Repository;
using PRM392_ShopClothes_Service.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PRM392_ShopClothes_Service.Service
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;


        public UserService(IMapper mapper, IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<string> AuthorizeUser(LoginView loginView)
        {
            var member = _unitOfWork.MemberRepository.Get(filter: m => m.Email == loginView.Email && m.Status == "Success").FirstOrDefault();
            if (member != null)
            {
                string token = GenerateToken(member);
                return token;
            }

            return null;
        }

        public async Task<PRM392_ShopClothes_Repository.Entities.Member?> GetUserByEmail(string email)
        {
            var member = _unitOfWork.MemberRepository.Get(filter: p => p.Email == email).FirstOrDefault();
            if (member == null)
            {
                // Handle case where member is not found
                throw new Exception("User not found.");
            }
            return member;
        }

        public async Task<PRM392_ShopClothes_Repository.Entities.Member?> GetUserById(long id)
        {
            var member = _unitOfWork.MemberRepository.Get(filter: p => p.MemberId == id).FirstOrDefault();
            if (member == null)
            {
                // Handle case where member is not found
                throw new Exception("User not found.");
            }
            return member;

        }

        public async Task<RegisterUserResponse> RegisterUser(RegisterUserRequest registerUserRequest)
        {
            var existingMember = _unitOfWork.MemberRepository.Get(filter: v => v.Email == registerUserRequest.Email).FirstOrDefault();
            if (existingMember != null)
            {
                // Handle case where member is not found
                throw new Exception("Email already exist.");
            }

            var member = _mapper.Map<PRM392_ShopClothes_Repository.Entities.Member>(registerUserRequest);
            member.RoleId = 2;
            member.Status = "Success";
            _unitOfWork.MemberRepository.Insert(member);
            _unitOfWork.Save();

            var registerResponse = _mapper.Map<RegisterUserResponse>(member);
            return registerResponse;
            
        }

        public async Task<PRM392_ShopClothes_Repository.Entities.Member[]> SearchUser(string? keyword, int? pageNumber = 1, int? pageSize = 10)
        {
            Expression<Func <PRM392_ShopClothes_Repository.Entities.Member, bool>> filter = p =>
                string.IsNullOrEmpty(keyword) ||
                p.UserName.Contains(keyword) ||
                p.Email.Contains(keyword);

            var users = _unitOfWork.MemberRepository.Get(
                filter: filter,
                includeProperties: "Role",
                pageIndex: pageNumber,
                pageSize: pageSize

                );
            return users.ToArray();
        }

        public async Task<UpdateUserResponse> UpdateMember(int id, UpdateUserRequest updateUserRequest)
        {
            var member = _unitOfWork.MemberRepository.Get(filter: p => p.MemberId == id).FirstOrDefault();

            if (member == null)
            {
                // Handle case where member is not found
                throw new Exception("User not found.");
            }

            // Map the updated properties from the request to the member entity
            _mapper.Map(updateUserRequest, member);

            // Save the changes to the repository
            _unitOfWork.MemberRepository.Update(member);
            _unitOfWork.Save();

            // Map the updated member entity back to the response
            var updateResponse = _mapper.Map<UpdateUserResponse>(member);
            return updateResponse;
        }



        //Generate Token
        private string GenerateToken(PRM392_ShopClothes_Repository.Entities.Member info)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, info.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds().ToString(), ClaimValueTypes.Integer64),
                new Claim("memberId", info.MemberId.ToString()),
                new Claim("email", info.Email),
                new Claim("name", info.UserName),
                new Claim("Phone", info.Phone)
            };

            // Add role claim if role information is available
            if (info.Role != null)
            {
                claims.Add(new Claim("role", info.Role.RoleName));
            }
            else
            {
                var role = _unitOfWork.RoleRepository.Get(filter: r => r.RoleId == info.RoleId).FirstOrDefault();
                if (role != null)
                {
                    claims.Add(new Claim("role", role.RoleName));
                }
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
