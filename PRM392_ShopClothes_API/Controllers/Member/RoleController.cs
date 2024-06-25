using Microsoft.AspNetCore.Mvc;
using PRM392_ShopClothes_Repository.Entities;
using PRM392_ShopClothes_Service.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PRM392_ShopClothes_Api.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateRole([FromBody] Role role)
        {
            try
            {
                var createdRole = await _roleService.CreateRole(role);
                return Ok(createdRole);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateRole([FromBody] Role role)
        {
            try
            {
                var updatedRole = await _roleService.UpdateRole(role);
                return Ok(updatedRole);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            try
            {
                var result = await _roleService.DeleteRole(id);
                if (result)
                {
                    return Ok(new { Message = "Role deleted successfully." });
                }
                return NotFound(new { Message = "Role not found." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            try
            {
                var role = await _roleService.GetRoleById(id);
                if (role == null)
                {
                    return NotFound(new { Message = "Role not found." });
                }
                return Ok(role);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllRoles()
        {
            try
            {
                var roles = await _roleService.GetAllRoles();
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
