using AutoMapper;
using PRM392_ShopClothes_Model.Model.Request;
using PRM392_ShopClothes_Model.Model.Response;
using PRM392_ShopClothes_Repository.Entities;
using PRM392_ShopClothes_Repository.Repository;
using PRM392_ShopClothes_Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392_ShopClothes_Service.Service
{
    public class CartService : ICartService

    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CartService(IUnitOfWork unitOfWork, IMapper mapper) { 
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task AddToCart(CartRequest cartRequest)
        {
            var cart = _unitOfWork.CartRepository.Get(c => c.MemberId == cartRequest.MemberId).FirstOrDefault();
            if (cart == null)
            {
                cart = new Cart
                {
                    MemberId = cartRequest.MemberId,
                    CreatedAt = DateTime.UtcNow,
                    ModifiedAt = DateTime.UtcNow,
                    Total = 0
                };

                _unitOfWork.CartRepository.Insert(cart);
                _unitOfWork.Save();
            }

            var cartItem = _unitOfWork.CartItemRepository.Get(ci => ci.CartId == cart.CartId
                                                           && ci.ProductId == cartRequest.ProductId
                                                           && ci.Size == cartRequest.Size).FirstOrDefault();
            if (cartItem == null)
            {
                cartItem = _mapper.Map<CartItem>(cartRequest);
                cartItem.CartId = cart.CartId;

                _unitOfWork.CartItemRepository.Insert(cartItem);
            }
            else
            {
                cartItem.Quantity += cartRequest.Quantity;
                cartItem.AddedAt = DateTime.UtcNow; // Update AddedAt time when quantity is incremented
                _unitOfWork.CartItemRepository.Update(cartItem);
            }

            var product = _unitOfWork.ProductRepository.GetById(cartRequest.ProductId);

            if (cartRequest.Quantity > product.UnitsInStock)
            {
                throw new Exception("Not enough stock available.");
            }

            if (product != null)
            {
                cart.Total += product.UnitPrice * cartRequest.Quantity;
            }

            cart.ModifiedAt = DateTime.UtcNow;
            _unitOfWork.CartRepository.Update(cart);

            _unitOfWork.Save();
            return Task.CompletedTask;
        }


        public async Task UpdateItemQuantity(CartRequest cartRequest)
        {
            var cart = _unitOfWork.CartRepository.Get(c => c.MemberId == cartRequest.MemberId).FirstOrDefault();
            if (cart == null) throw new Exception("Cart not found.");

            var cartItem = _unitOfWork.CartItemRepository.Get(ci => ci.CartId == cart.CartId && ci.ProductId == cartRequest.ProductId).FirstOrDefault();
            if (cartItem == null) throw new Exception("Cart item not found.");

            var product = _unitOfWork.ProductRepository.GetById(cartRequest.ProductId);
            if (product == null) throw new Exception("Product not found.");

            if (cartRequest.Quantity > product.UnitsInStock)
            {
                throw new Exception("Not enough stock available.");
            }

            cart.Total -= cartItem.Quantity * product.UnitPrice;
            cartItem.Quantity = cartRequest.Quantity;
            cart.Total += cartItem.Quantity * product.UnitPrice;
            cartItem.Size = cartRequest.Size;

            _unitOfWork.CartItemRepository.Update(cartItem);
            cart.ModifiedAt = DateTime.UtcNow;
            _unitOfWork.CartRepository.Update(cart);

            _unitOfWork.Save();
        }

        public async Task RemoveItem(RemoveItemRequest removeItemRequest)
        {
            var cart = _unitOfWork.CartRepository.Get(c => c.MemberId == removeItemRequest.MemberId).FirstOrDefault();
            if (cart == null) throw new Exception("Cart not found.");

            var cartItem = _unitOfWork.CartItemRepository.Get(ci => ci.CartId == cart.CartId 
                                                           && ci.ProductId == removeItemRequest.ProductId).FirstOrDefault();
            if (cartItem == null) throw new Exception("Cart item not found.");

            var product = _unitOfWork.ProductRepository.GetById(removeItemRequest.ProductId);
            if (product == null) throw new Exception("Product not found.");

            cart.Total -= cartItem.Quantity * product.UnitPrice;

            _unitOfWork.CartItemRepository.Delete(cartItem);
            cart.ModifiedAt = DateTime.UtcNow;
            _unitOfWork.CartRepository.Update(cart);

            _unitOfWork.Save();
        }

        public async Task ClearCart(int memberId)
        {
            var cart = _unitOfWork.CartRepository.Get(c => c.MemberId == memberId).FirstOrDefault();
            if (cart == null) throw new Exception("Cart not found.");

            var cartItems = _unitOfWork.CartItemRepository.Get(ci => ci.CartId == cart.CartId).ToList();
            foreach (var item in cartItems)
            {
                _unitOfWork.CartItemRepository.Delete(item);
            }

            cart.Total = 0;
            cart.ModifiedAt = DateTime.UtcNow;
            _unitOfWork.CartRepository.Update(cart);

            _unitOfWork.Save();
        }

        public async Task<CartResponse> GetCart(int memberId)
        {
            var cart = _unitOfWork.CartRepository.Get(c => c.MemberId == memberId, includeProperties: "CartItems.Product").FirstOrDefault();
            if (cart == null) throw new Exception("Cart not found.");

            var cartDto = _mapper.Map<CartResponse>(cart);
            return cartDto;
        }

    }
}
