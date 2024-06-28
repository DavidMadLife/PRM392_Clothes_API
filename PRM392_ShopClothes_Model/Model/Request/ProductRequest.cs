using Microsoft.AspNetCore.Http;

namespace PRM392_ShopClothes_Model.Model.Request
{
    public class ProductRequest
    {
        public int CategoryId { get; set; }
        public int ProviderId { get; set; }
        public string ProductName { get; set; }
        public double Weight { get; set; }
        public double UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public IFormFile ImgFile { get; set; } // New property for image file
    }
}
