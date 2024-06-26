using PRM392_ShopClothes_Model.Model.Request;
using PRM392_ShopClothes_Model.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392_ShopClothes_Service.Interface
{
    public interface IOrderService
    {
        Task Checkout(OrderRequest orderRequest);

        Task<IEnumerable<OrderResponse>> GetOrdersByMemberId(int id);

        Task ConfirmOrder(int id);

        Task RejectOrder(int id);

        List<OrderResponse> GetOrderByStatusPending(string? keyword, int pageIndex, int pageSize);

        List<OrderResponse> GetOrderByStatusConfirmed(string? keyword, int pageIndex, int pageSize);

        List<OrderResponse> GetOrderByStatusRejected(string? keyword, int pageIndex, int pageSize);

        double GetTotalConfirmedOrdersAmount();

        int CountOrder();
    }
}
