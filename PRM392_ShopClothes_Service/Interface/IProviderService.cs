using PRM392_ShopClothes_Model.Model.Request;
using PRM392_ShopClothes_Model.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392_ShopClothes_Service.Interface
{
    public interface IProviderService
    {
        Task<ProviderResponse> GetProviderByIdAsync(int id);
        Task<IEnumerable<ProviderResponse>> GetAllProvidersAsync();
        Task<(IEnumerable<ProviderResponse>, int)> SearchProvidersAsync(string providerName, int pageIndex, int pageSize);
        Task<ProviderResponse> CreateProviderAsync(ProviderRequest providerRequest);
        Task<ProviderResponse> UpdateProviderAsync(int id, ProviderRequest providerRequest);
        Task<bool> DeleteProviderAsync(int id);
    }
}
