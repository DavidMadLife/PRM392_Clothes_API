using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392_ShopClothes_Model.Model.Request
{
    public class ChangePasswordRequest
    {
        public string currentPassword { get; set; }
        public string newPassword { get; set; }
    }
}
