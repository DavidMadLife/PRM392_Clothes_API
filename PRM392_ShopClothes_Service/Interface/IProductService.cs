using PRM392_ShopClothes_Model.Model.Request;
using PRM392_ShopClothes_Model.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392_ShopClothes_Service.Interface
{
    public interface IProductService
    {
        Task<ProductResponse> GetProductByIdAsync(int id);
        Task<IEnumerable<ProductResponse>> GetAllProductsAsync();
        Task<(IEnumerable<ProductResponse>, int)> SearchProductsAsync(string productName, int? providerId, double? unitPrice, double? weight, int? categoryId, int pageIndex, int pageSize);
        Task<ProductResponse> CreateProductAsync(ProductRequest productRequest);
        Task<ProductResponse> UpdateProductAsync(int id, ProductRequest productRequestDTO);
        Task<bool> DeleteProductAsync(int id);
    }
}
