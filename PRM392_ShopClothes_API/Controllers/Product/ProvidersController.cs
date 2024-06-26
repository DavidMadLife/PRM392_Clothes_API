using Microsoft.AspNetCore.Mvc;
using PRM392_ShopClothes_Model.Model.Request;
using PRM392_ShopClothes_Service.Interface;

namespace PRM392_ShopClothes_API.Controllers.Product
{
    [ApiController]
    [Route("api/providers")]
    public class ProvidersController : ControllerBase
    {
        private readonly IProviderService _providerService;

        public ProvidersController(IProviderService providerService)
        {
            _providerService = providerService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProviderById(int id)
        {
            var provider = await _providerService.GetProviderByIdAsync(id);
            if (provider == null)
                return NotFound();

            return Ok(provider);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProviders()
        {
            var providers = await _providerService.GetAllProvidersAsync();
            return Ok(providers);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchProviders([FromQuery] string? providerName, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
        {
            var (providers, totalCount) = await _providerService.SearchProvidersAsync(providerName, pageIndex, pageSize);
            return Ok(new { Providers = providers, TotalCount = totalCount });
        }

        [HttpPost]
        public async Task<IActionResult> CreateProvider([FromBody] ProviderRequest providerRequest)
        {
            var createdProvider = await _providerService.CreateProviderAsync(providerRequest);
            return CreatedAtAction(nameof(GetProviderById), new { id = createdProvider.ProviderId }, createdProvider);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProvider(int id, [FromBody] ProviderRequest providerRequest)
        {
            var updatedProvider = await _providerService.UpdateProviderAsync(id, providerRequest);
            if (updatedProvider == null)
                return NotFound();

            return Ok(updatedProvider);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProvider(int id)
        {
            var result = await _providerService.DeleteProviderAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
