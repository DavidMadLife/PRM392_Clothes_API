using System.ComponentModel.DataAnnotations;

namespace PRM392_ShopClothes_Model.Model.Request
{
    public class RegisterUserRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        public string Address { get; set; }
        [Required]
        public int RoleId { get; set; }
    }
}
