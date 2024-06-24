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
    public class ProviderService : IProviderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProviderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProviderResponse> GetProviderByIdAsync(int id)
        {
            var provider = _unitOfWork.ProviderRepository.GetById(id);
            if (provider == null)
                return null;

            return _mapper.Map<ProviderResponse>(provider);
        }

        public async Task<IEnumerable<ProviderResponse>> GetAllProvidersAsync()
        {
            var providers = _unitOfWork.ProviderRepository.Get();
            return _mapper.Map<IEnumerable<ProviderResponse>>(providers);
        }

        public async Task<(IEnumerable<ProviderResponse>, int)> SearchProvidersAsync(string providerName, int pageIndex, int pageSize)
        {
            Expression<Func<Provider, bool>> filter = p =>
                string.IsNullOrEmpty(providerName) || p.ProviderName.Contains(providerName);

            var (providers, totalCount) = _unitOfWork.ProviderRepository.GetWithCount(filter, null, "", pageIndex, pageSize);

            var providerDTOs = _mapper.Map<IEnumerable<ProviderResponse>>(providers);

            return (providerDTOs, totalCount);
        }

        public async Task<ProviderResponse> CreateProviderAsync(ProviderRequest providerRequest)
        {
            var provider = _mapper.Map<Provider>(providerRequest);
            _unitOfWork.ProviderRepository.Insert(provider);
            _unitOfWork.Save();

            return _mapper.Map<ProviderResponse>(provider);
        }

        public async Task<ProviderResponse> UpdateProviderAsync(int id, ProviderRequest providerRequest)
        {
            var existingProvider = _unitOfWork.ProviderRepository.GetById(id);
            if (existingProvider == null)
                return null;

            _mapper.Map(providerRequest, existingProvider);
            _unitOfWork.ProviderRepository.Update(existingProvider);
            _unitOfWork.Save();

            return _mapper.Map<ProviderResponse>(existingProvider);
        }

        public async Task<bool> DeleteProviderAsync(int id)
        {
            var provider = _unitOfWork.ProviderRepository.GetById(id);
            if (provider == null)
                return false;

            _unitOfWork.ProviderRepository.Delete(provider);
            _unitOfWork.Save();

            return true;
        }
    }
}
