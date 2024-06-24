using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392_ShopClothes_Model.Model.Response
{
    public class ProductResponse
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public int ProviderId { get; set; }
        public string ProductName { get; set; }
        public double Weight { get; set; }
        public double UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public string Img { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string ProviderName { get; set; }
    }
}
