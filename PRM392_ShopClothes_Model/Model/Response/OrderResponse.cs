using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392_ShopClothes_Model.Model.Response
{
    public class OrderResponse
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderCode { get; set; }
        public DateTime ShippedDate { get; set; }
        public IEnumerable<OrderDetaiResponse> OrderDetails { get; set; }
        public string Status { get; set; }
        public double Total { get; set; }
    }
}
