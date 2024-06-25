using PRM392_ShopClothes_Repository.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PRM392_ShopClothes_Service.Interface
{
    public interface IRoleService
    {
        Task<Role> CreateRole(Role role);
        Task<Role> UpdateRole(Role role);
        Task<bool> DeleteRole(int roleId);
        Task<Role> GetRoleById(int roleId);
        Task<List<Role>> GetAllRoles();
    }
}
