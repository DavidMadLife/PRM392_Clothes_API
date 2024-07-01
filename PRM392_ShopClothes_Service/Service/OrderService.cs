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
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task Checkout(OrderRequest orderRequest)
        {
            try
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
                    CartId = cart.CartId,
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
                        Size = item.Size,
                        Discount = 0
                    };

                    _unitOfWork.OrderDetailRepository.Insert(orderDetail);
                }

                cart.Total = 0;
                cart.CartItems.Clear();
                _unitOfWork.CartRepository.Update(cart);

                _unitOfWork.Save();
            }catch(Exception ex)
            {
                Console.WriteLine("Lỗi khi lưu thay đổi vào cơ sở dữ liệu: " + ex.Message);
                throw;
            }

        }

        public async Task<IEnumerable<OrderResponse>> GetOrdersByMemberId(int id)
        {
            var orders = _unitOfWork.OrderRepository.Get(
                filter: o => o.Cart.MemberId == id,
                includeProperties: "OrderDetails,OrderDetails.Product"
            );

            var orderResponse = _mapper.Map<IEnumerable<OrderResponse>>(orders);

            return orderResponse;
        }

        public async Task ConfirmOrder(int id)
        {
            try
            {
                var order = _unitOfWork.OrderRepository.Get(o => o.OrderId == id,
                                                            includeProperties: "OrderDetails,OrderDetails.Product,Cart.Member").FirstOrDefault();
                if (order == null)
                {
                    throw new Exception("Order not found.");
                }

                if (order.Status == "Confirmed")
                {
                    throw new Exception("Order has already been confirmed.");
                }

                order.Status = "Confirmed";
                _unitOfWork.OrderRepository.Update(order);

                _unitOfWork.Save();

                var orderResponse = new OrderResponse
                {
                    OrderCode = order.OrderCode,
                    OrderDate = order.OrderDate,
                    ShippedDate = order.ShippedDate,
                    Total = order.Total,
                    OrderDetails = order.OrderDetails.Select(od => new OrderDetaiResponse
                    {
                        ProductId = od.ProductId,
                        ProductName = od.Product.ProductName,
                        UnitPrice = od.UnitPrice,
                        Quantity = od.Quantity,
                        Size = od.Size,
                    }).ToList()
                };

                var user = _unitOfWork.MemberRepository.GetById(order.Cart.MemberId);

                await _emailService.SendConfirmedOrderEmailAsync(user.Email, orderResponse);
                Console.WriteLine(user.Email);

                foreach (var orderDetail in order.OrderDetails)
                {
                    var product = _unitOfWork.ProductRepository.GetById(orderDetail.ProductId);
                    if (product != null)
                    {
                        if (product.UnitsInStock < orderDetail.Quantity)
                        {
                            throw new Exception($"Insufficient stock for product {product.ProductName}. Available: {product.UnitsInStock}, Ordered: {orderDetail.Quantity}");
                        }

                        product.UnitsInStock -= orderDetail.Quantity;
                        _unitOfWork.ProductRepository.Update(product);
                    }
                }

                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi cập nhật trạng thái đơn hàng: " + ex.Message);
                throw;
            }
        }

        public async Task RejectOrder(int id)
        {
            try
            {
                var order = _unitOfWork.OrderRepository.Get(o => o.OrderId == id,
                                                            includeProperties: "OrderDetails,OrderDetails.Product,Cart.Member").FirstOrDefault();
                if (order == null)
                {
                    throw new Exception("Order not found.");
                }

                if (order.Status == "Rejected")
                {
                    throw new Exception("Order has already been Rejected.");
                }

                order.Status = "Rejected";
                _unitOfWork.OrderRepository.Update(order);

                _unitOfWork.Save();

                var orderResponse = new OrderResponse
                {
                    OrderCode = order.OrderCode,
                    OrderDate = order.OrderDate,
                    ShippedDate = order.ShippedDate,
                    Total = order.Total,
                    OrderDetails = order.OrderDetails.Select(od => new OrderDetaiResponse
                    {
                        ProductId = od.ProductId,
                        ProductName = od.Product.ProductName,
                        UnitPrice = od.UnitPrice,
                        Quantity = od.Quantity,
                        Size = od.Size,
                    }).ToList()
                };

                var user = _unitOfWork.MemberRepository.GetById(order.Cart.MemberId);

                await _emailService.SendConfirmedOrderEmailAsync(user.Email, orderResponse);
                Console.WriteLine(user.Email);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi cập nhật trạng thái đơn hàng: " + ex.Message);
                throw;
            }
        }

        public List<OrderResponse> GetOrderByStatusPending(string? keyword, int pageIndex, int pageSize)
        {
            try
            {

                Expression<Func<Order, bool>> filter = s => (string.IsNullOrEmpty(keyword) ||
                                                            s.OrderCode.Contains(keyword))
                                                            && s.Status == "Pending";

                var listOrder = _unitOfWork.OrderRepository.Get(
                    filter: filter,
                    includeProperties: "OrderDetails,OrderDetails.Product",
                    pageIndex: pageIndex,
                    pageSize: pageSize
                );
                var OrderResponses = _mapper.Map<List<OrderResponse>>(listOrder);
                return OrderResponses;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi cập nhật trạng thái đơn hàng: " + ex.Message);
                throw;
            }
        }

        public List<OrderResponse> GetOrderByStatusConfirmed(string? keyword, int pageIndex, int pageSize)
        {
            try
            {

                Expression<Func<Order, bool>> filter = s => (string.IsNullOrEmpty(keyword) ||
                                                            s.OrderCode.Contains(keyword))
                                                            && s.Status == "Confirmed";

                var listOrder = _unitOfWork.OrderRepository.Get(
                    filter: filter,
                    includeProperties: "OrderDetails,OrderDetails.Product",
                    pageIndex: pageIndex,
                    pageSize: pageSize
                );
                var OrderResponses = _mapper.Map<List<OrderResponse>>(listOrder);
                return OrderResponses;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi cập nhật trạng thái đơn hàng: " + ex.Message);
                throw;
            }
        }
        public List<OrderResponse> GetOrderByStatusRejected(string? keyword, int pageIndex, int pageSize)
        {
            try
            {

                Expression<Func<Order, bool>> filter = s => (string.IsNullOrEmpty(keyword) ||
                                                            s.OrderCode.Contains(keyword))
                                                            && s.Status == "Rejected";

                var listOrder = _unitOfWork.OrderRepository.Get(
                    filter: filter,
                    includeProperties: "OrderDetails,OrderDetails.Product",
                    pageIndex: pageIndex,
                    pageSize: pageSize
                );
                var OrderResponses = _mapper.Map<List<OrderResponse>>(listOrder);
                return OrderResponses;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi cập nhật trạng thái đơn hàng: " + ex.Message);
                throw;
            }
        }

        public double GetTotalConfirmedOrdersAmount()
        {
            try
            {
                var confirmedOrders = _unitOfWork.OrderRepository.Get(
                    filter: o => o.Status == "Confirmed"
                );

                var totalAmount = confirmedOrders.Sum(o => o.Total);

                return totalAmount;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi tính tổng số tiền đơn hàng đã xác nhận: " + ex.Message);
                throw;
            }
        }

        public int CountOrder()
        {
            try
            {
                var confirmedOrdersCount = _unitOfWork.OrderRepository.Get(
                    filter: o => o.Status == "Confirmed"
                ).Count();

                return confirmedOrdersCount;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi đếm số lượng đơn hàng đã xác nhận: " + ex.Message);
                throw;
            }
        }
    }
}
