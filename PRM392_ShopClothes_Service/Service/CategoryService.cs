using AutoMapper;
using PRM392_ShopClothes_Model.Model.Request;
using PRM392_ShopClothes_Model.Model.Response;
using PRM392_ShopClothes_Repository.Entities;
using PRM392_ShopClothes_Repository.Repository;
using PRM392_ShopClothes_Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PRM392_ShopClothes_Service.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CategoryResponse> GetCategoryByIdAsync(int id)
        {
            var category = _unitOfWork.CategoryRepository.GetById(id);
            if (category == null)
                return null;

            return _mapper.Map<CategoryResponse>(category);
        }

        public async Task<IEnumerable<CategoryResponse>> GetAllCategoriesAsync()
        {
            var categories = _unitOfWork.CategoryRepository.Get();
            return _mapper.Map<IEnumerable<CategoryResponse>>(categories);
        }

        public async Task<(IEnumerable<CategoryResponse>, int)> SearchCategoriesAsync(string categoryName, int pageIndex, int pageSize)
        {
            Expression<Func<Category, bool>> filter = c =>
                string.IsNullOrEmpty(categoryName) || c.CategoryName.Contains(categoryName);

            var (categories, totalCount) = _unitOfWork.CategoryRepository.GetWithCount(filter, null, "", pageIndex, pageSize);

            var categoryDTOs = _mapper.Map<IEnumerable<CategoryResponse>>(categories);

            return (categoryDTOs, totalCount);
        }

        public async Task<CategoryResponse> CreateCategoryAsync(CategoryRequest categoryRequest)
        {
            var category = _mapper.Map<Category>(categoryRequest);
            _unitOfWork.CategoryRepository.Insert(category);
            _unitOfWork.Save();

            return _mapper.Map<CategoryResponse>(category);
        }

        public async Task<CategoryResponse> UpdateCategoryAsync(int id, CategoryRequest categoryRequestDTO)
        {
            var existingCategory = _unitOfWork.CategoryRepository.GetById(id);
            if (existingCategory == null)
                return null;

            _mapper.Map(categoryRequestDTO, existingCategory);
            _unitOfWork.CategoryRepository.Update(existingCategory);
            _unitOfWork.Save();

            return _mapper.Map<CategoryResponse>(existingCategory);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = _unitOfWork.CategoryRepository.GetById(id);
            if (category == null)
                return false;

            _unitOfWork.CategoryRepository.Delete(category);
            _unitOfWork.Save();

            return true;
        }
    }
}
