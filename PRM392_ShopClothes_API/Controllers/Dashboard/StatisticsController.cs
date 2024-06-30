using Microsoft.AspNetCore.Mvc;
using PRM392_ShopClothes_Service.Interface;

namespace PRM392_ShopClothes_API.Controllers.Dashboard
{
    [Route("api/statistics")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        [HttpGet("totals")]
        public IActionResult GetTotals()
        {
            var response = new
            {
                TotalMembers = _statisticsService.GetTotalMembers(),
                TotalProducts = _statisticsService.GetTotalProducts(),
                TotalTransactions = _statisticsService.GetTotalTransactions(),
                TotalRevenue = _statisticsService.GetTotalRevenue()
            };

            return Ok(response);
        }
    }
}
