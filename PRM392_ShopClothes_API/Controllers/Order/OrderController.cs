using Microsoft.AspNetCore.Mvc;
using PRM392_ShopClothes_Model.Model.Request;
using PRM392_ShopClothes_Service.Interface;
using System.Net;

namespace PRM392_ShopClothes_API.Controllers.Order
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

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

        [HttpGet("getOrderByMemberId/{id}")]
        public async Task<IActionResult> GetOrdersByMemberId(int id)
        {
            try
            {
                var orders = await _orderService.GetOrdersByMemberId(id);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpPatch("confirmOrder/{id}")]
        public async Task<IActionResult> ConfirmOrder(int id)
        {
            try
            {
                await _orderService.ConfirmOrder(id);
                return Ok(new { message = "Order confirmed successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPatch("rejectOrder/{id}")]
        public async Task<IActionResult> RejectOrder(int id)
        {
            try
            {
                await _orderService.RejectOrder(id);
                return Ok(new { message = "Order rejected successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("getOrderByStatusPending")]
        public IActionResult GetOrderByStatusPending(string? keyword, int pageIndex, int pageSize)
        {
            try
            {
                var order = _orderService.GetOrderByStatusPending(keyword, pageIndex, pageSize);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("getOrderByStatusConfirmed")]
        public IActionResult GetOrderByStatusConfirmed(string? keyword, int pageIndex, int pageSize)
        {
            try
            {
                var order = _orderService.GetOrderByStatusConfirmed(keyword, pageIndex, pageSize);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("getOrderByStatusRejected")]
        public IActionResult GetOrderByStatusRejected(string? keyword, int pageIndex, int pageSize)
        {
            try
            {
                var order = _orderService.GetOrderByStatusRejected(keyword, pageIndex, pageSize);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("totalConfirmedOrdersAmount")]
        public IActionResult GetTotalConfirmedOrdersAmount()
        {
            try
            {
                var totalAmount = _orderService.GetTotalConfirmedOrdersAmount();
                return Ok(totalAmount);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Lỗi khi tính tổng số tiền đơn hàng đã xác nhận: " + ex.Message);
            }
        }

        [HttpGet("countConfirmedOrders")]
        public IActionResult GetConfirmedOrdersCount()
        {
            try
            {
                var confirmedOrdersCount = _orderService.CountOrder();
                return Ok(confirmedOrdersCount);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Lỗi khi đếm số lượng đơn hàng đã xác nhận: " + ex.Message);
            }
        }
    }
}
