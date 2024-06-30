using PRM392_ShopClothes_Repository.Repository;
using PRM392_ShopClothes_Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392_ShopClothes_Service.Service
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StatisticsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public int GetTotalMembers()
        {
            return _unitOfWork.MemberRepository.Count();
        }

        public int GetTotalProducts()
        {
            return _unitOfWork.ProductRepository.Count();
        }

        public int GetTotalTransactions()
        {
            return _unitOfWork.OrderDetailRepository.Count();
        }

        public double GetTotalRevenue()
        {
            var orderDetails = _unitOfWork.OrderDetailRepository.Get();
            return orderDetails.Sum(od => od.UnitPrice * od.Quantity);
        }
    }
}
