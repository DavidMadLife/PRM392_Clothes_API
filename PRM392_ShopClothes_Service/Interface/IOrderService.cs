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

        Task<IEnumerable<OrderResponse>> GetOrdersByMemberId(int memberId);
    }
}
