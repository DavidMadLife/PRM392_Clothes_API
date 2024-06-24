using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392_ShopClothes_Model.Model.Response
{
    public class CartResponse
    {
        public int CartId { get; set; }
        public int MemberId { get; set; }
        public List<CartItemResponse> Items { get; set; }
        public double Total { get; set; }
    }
}
