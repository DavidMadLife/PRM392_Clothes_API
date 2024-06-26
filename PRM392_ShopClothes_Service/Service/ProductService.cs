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
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductResponse> GetProductByIdAsync(int id)
        {
            var product = _unitOfWork.ProductRepository.GetById(id);
            if (product == null)
                return null;

            return _mapper.Map<ProductResponse>(product);
        }

        public async Task<IEnumerable<ProductResponse>> GetAllProductsAsync()
        {
            var products = _unitOfWork.ProductRepository.Get();
            return _mapper.Map<IEnumerable<ProductResponse>>(products);
        }

        public async Task<(IEnumerable<ProductResponse>, int)> SearchProductsAsync(string productName, int? providerId, double? unitPrice, double? weight, int? categoryId, int pageIndex, int pageSize)
        {
            Expression<Func<Product, bool>> filter = p =>
                (string.IsNullOrEmpty(productName) || p.ProductName.Contains(productName)) &&
                (!providerId.HasValue || p.ProviderId == providerId) &&
                (!unitPrice.HasValue || p.UnitPrice == unitPrice) &&
                (!weight.HasValue || p.Weight == weight) &&
                (!categoryId.HasValue || p.CategoryId == categoryId);

            var (products, totalCount) = _unitOfWork.ProductRepository.GetWithCount(filter, null, "Category,Provider", pageIndex, pageSize);

            var productDTOs = _mapper.Map<IEnumerable<ProductResponse>>(products);

            return (productDTOs, totalCount);
        }

        public async Task<ProductResponse> CreateProductAsync(ProductRequest productRequest)
        {
            var product = _mapper.Map<Product>(productRequest);
            _unitOfWork.ProductRepository.Insert(product);
            _unitOfWork.Save();

            return _mapper.Map<ProductResponse>(product);
        }

        public async Task<ProductResponse> UpdateProductAsync(int id, ProductRequest productRequest)
        {
            var existingProduct = _unitOfWork.ProductRepository.GetById(id);
            if (existingProduct == null)
                return null;

            _mapper.Map(productRequest, existingProduct);
            _unitOfWork.ProductRepository.Update(existingProduct);
            _unitOfWork.Save();

            return _mapper.Map<ProductResponse>(existingProduct);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = _unitOfWork.ProductRepository.GetById(id);
            if (product == null)
                return false;

            _unitOfWork.ProductRepository.Delete(product);
            _unitOfWork.Save();

            return true;
        }
    }
}
