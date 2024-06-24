using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392_ShopClothes_Model.Model.Response
{
    public class UpdateUserResponse
    {
        public int MemberId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int RoleId { get; set; }
        public string Status { get; set; }
    }
}
