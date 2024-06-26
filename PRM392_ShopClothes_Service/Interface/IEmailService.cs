using PRM392_ShopClothes_Model.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392_ShopClothes_Service.Interface
{
    public interface IEmailService
    {
        Task SendConfirmedOrderEmailAsync(string toEmail, OrderResponse orderResponse);
        Task SendRejectedOrderEmailAsync(string toEmail, OrderResponse orderResponse);
    }
}
