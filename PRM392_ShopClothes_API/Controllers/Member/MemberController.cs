using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRM392_ShopClothes_Model.Model.Request;
using PRM392_ShopClothes_Model.Model.Response;
using PRM392_ShopClothes_Service.Interface;

namespace PRM392_ShopClothes_API.Controllers.Member
{
    [Route("api/members")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IUserService _userService;

        public MemberController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginView loginView)
        {
            var token = await _userService.AuthorizeUser(loginView);
            if (token != null)
            {
                return Ok(new { Token = token });
            }
            else
            {
                return Unauthorized(new { Message = "Invalid email or password." });
            }
        }

        [HttpGet("search")]

        public async Task<IActionResult> SearchUser([FromQuery] string? keyword, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var users = await _userService.SearchUser(keyword, pageNumber, pageSize);
            return Ok(users);
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegisterUserResponse>> RegisterUser([FromBody] RegisterUserRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var user = await _userService.RegisterUser(request);
                return CreatedAtAction(nameof(GetUserById), new { id = user.MemberId }, user);
            }
            
            catch (Exception ex)
            {
                // Handle other exceptions
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("{id:int}")]

        public async Task<IActionResult> GetUserById(long id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserRequest updateUserRequest)
        {
            try
            {
                var response = await _userService.UpdateMember(id, updateUserRequest);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            try
            {
                var result = await _userService.DeleteUser(id);
                if (result)
                {
                    return Ok(new { message = "User deleted successfully." });
                }
                return NotFound(new { message = "User not found." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPut("ChangePassword/{id}")]
        public async Task<IActionResult> ChangePassword(long id, [FromBody] ChangePasswordRequest request)
        {
            try
            {
                var result = await _userService.ChangePassword(id, request.currentPassword, request.newPassword);
                if (result)
                {
                    return Ok(new { message = "Password changed successfully." });
                }
                return BadRequest(new { message = "Failed to change password." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


    }
}
