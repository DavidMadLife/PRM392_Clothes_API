using Microsoft.AspNetCore.Mvc;
using PRM392_ShopClothes_Model.Model.Request;
using PRM392_ShopClothes_Service.Interface;

namespace PRM392_ShopClothes_API.Controllers.Order
{
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // Existing endpoints...

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] OrderRequest orderRequest)
        {
            try
            {
                await _orderService.Checkout(orderRequest);
                return Ok(new { message = "Checkout successful" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("orders/{memberId}")]
        public async Task<IActionResult> GetOrdersByMemberId(int memberId)
        {
            try
            {
                var orders = await _orderService.GetOrdersByMemberId(memberId);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
