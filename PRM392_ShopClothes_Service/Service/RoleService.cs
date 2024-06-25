using PRM392_ShopClothes_Repository.Entities;
using PRM392_ShopClothes_Repository.Repository;
using PRM392_ShopClothes_Service.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRM392_ShopClothes_Service.Service
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Role> CreateRole(Role role)
        {
            _unitOfWork.RoleRepository.Insert(role);
            _unitOfWork.Save();
            return role;
        }

        public async Task<Role> UpdateRole(Role role)
        {
            var existingRole = _unitOfWork.RoleRepository.GetById(role.RoleId);
            if (existingRole == null)
            {
                throw new Exception("Role not found.");
            }

            existingRole.RoleName = role.RoleName;
            _unitOfWork.RoleRepository.Update(existingRole);
            _unitOfWork.Save();

            return existingRole;
        }

        public async Task<bool> DeleteRole(int roleId)
        {
            var role = _unitOfWork.RoleRepository.GetById(roleId);
            if (role == null)
            {
                throw new Exception("Role not found.");
            }

            _unitOfWork.RoleRepository.Delete(role);
            _unitOfWork.Save();
            return true;
        }

        public async Task<Role> GetRoleById(int roleId)
        {
            var role = _unitOfWork.RoleRepository.GetById(roleId);
            return role;
        }

        public async Task<List<Role>> GetAllRoles()
        {
            var roles = _unitOfWork.RoleRepository.Get().ToList();
            return roles;
        }
    }
}
