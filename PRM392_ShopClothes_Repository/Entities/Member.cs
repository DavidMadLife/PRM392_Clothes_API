using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PRM392_ShopClothes_Repository.Entities
{
    [Table("Member")]
    public class Member
    {
        [Key]
        public int MemberId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Password { get; set; }
        [Required]
        public int RoleId { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Status { get; set; }
        

        [ForeignKey("RoleId")]
        [JsonIgnore]        
        
        public Role Role { get; set; }
    }
}
