using Microsoft.AspNetCore.Mvc;
using PRM392_ShopClothes_Model.Model.Request;
using PRM392_ShopClothes_Service.Interface;
using PRM392_ShopClothes_Service.Service;

namespace PRM392_ShopClothes_API.Controllers.CartController
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart(CartRequest cartRequest)
        {
            try
            {
                await _cartService.AddToCart(cartRequest);
                return Ok(new { message = "Product added to cart successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("update-quantity")]
        public async Task<IActionResult> UpdateItemQuantity(CartRequest cartRequest)
        {
            try
            {
                await _cartService.UpdateItemQuantity(cartRequest);
                return Ok(new { message = "Cart item quantity updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("remove")]
        public async Task<IActionResult> RemoveItem(RemoveItemRequest removeItemRequest)
        {
            try
            {
                await _cartService.RemoveItem(removeItemRequest);
                return Ok(new { message = "Cart item removed successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("clear/{memberId}")]
        public async Task<IActionResult> ClearCart(int memberId)
        {
            try
            {
                await _cartService.ClearCart(memberId);
                return Ok(new { message = "Cart cleared successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("{memberId}")]
        public async Task<IActionResult> GetCart(int memberId)
        {
            try
            {
                var cart = await _cartService.GetCart(memberId);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }

}
