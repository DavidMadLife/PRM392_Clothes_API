using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392_ShopClothes_Service.Interface
{
    public interface IStatisticsService
    {
        int GetTotalMembers();
        int GetTotalProducts();
        int GetTotalTransactions();
        double GetTotalRevenue();
    }
}
