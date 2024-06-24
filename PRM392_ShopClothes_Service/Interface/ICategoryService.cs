using PRM392_ShopClothes_Model.Model.Request;
using PRM392_ShopClothes_Model.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392_ShopClothes_Service.Interface
{
    public interface ICategoryService
    {
        Task<CategoryResponse> GetCategoryByIdAsync(int id);
        Task<IEnumerable<CategoryResponse>> GetAllCategoriesAsync();
        Task<(IEnumerable<CategoryResponse>, int)> SearchCategoriesAsync(string categoryName, int pageIndex, int pageSize);
        Task<CategoryResponse> CreateCategoryAsync(CategoryRequest categoryRequest);
        Task<CategoryResponse> UpdateCategoryAsync(int id, CategoryRequest categoryRequest);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
