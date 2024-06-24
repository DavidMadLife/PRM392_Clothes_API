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
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Checkout(OrderRequest orderRequest)
        {

            Random rand = new Random();
            int randomNumber = rand.Next(10000, 99999);

            var cart = _unitOfWork.CartRepository.Get(c => c.MemberId == orderRequest.MemberId,
                                                      includeProperties: "CartItems.Product").FirstOrDefault();
            if (cart == null || !cart.CartItems.Any())
            {
                throw new Exception("Cart is empty or not found.");
            }

            

            var order = new Order
            {
                CarId = cart.CartId,
                OrderDate = DateTime.UtcNow,
                OrderCode = "ORD" + randomNumber.ToString("D5"),
                ShippedDate = DateTime.UtcNow.AddDays(7),
                Total = cart.Total,
                Status = "Pending"
            };

            while (_unitOfWork.OrderRepository.Get(filter: cod => cod.OrderCode == order.OrderCode).Any())
            {
                randomNumber = new Random().Next(10000, 99999);
                order.OrderCode = "ORD" + randomNumber.ToString("D5");
            }

            _unitOfWork.OrderRepository.Insert(order);
            _unitOfWork.Save();

            foreach (var item in cart.CartItems)
            {
                var orderDetail = new OrderDetail
                {
                    OrderId = order.OrderId,
                    ProductId = item.ProductId,
                    UnitPrice = item.Product.UnitPrice,
                    Quantity = item.Quantity,
                    Discount = 0
                };

                _unitOfWork.OrderDetailRepository.Insert(orderDetail);
            }

            cart.Total = 0;
            cart.CartItems.Clear();
            _unitOfWork.CartRepository.Update(cart);

            _unitOfWork.Save();
        }

        public async Task<IEnumerable<OrderResponse>> GetOrdersByMemberId(int memberId)
        {
            var orders = _unitOfWork.OrderRepository.Get(
                filter: o => o.Cart.MemberId == memberId,
                includeProperties: "Cart,OrderDetails,OrderDetails.Product"
            );

            var orderResponse = _mapper.Map<IEnumerable<OrderResponse>>(orders);

            return orderResponse;
        }
    }
}
