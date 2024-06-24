using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392_ShopClothes_Model.Model.Request
{
    public class CartRequest
    {
        public int MemberId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
