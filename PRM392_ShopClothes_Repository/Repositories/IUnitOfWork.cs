


using PRM392_ShopClothes_Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392_ShopClothes_Repository.Repository
{
    public interface IUnitOfWork
    {
        public IGenericRepository<Category> CategoryRepository { get; }
        public IGenericRepository<Member> MemberRepository { get; }
        public IGenericRepository<Order> OrderRepository { get; }
        public IGenericRepository<OrderDetail> OrderDetailRepository { get; }
        public IGenericRepository<Product> ProductRepository { get; }
        public IGenericRepository<Provider> ProviderRepository { get; }
        public IGenericRepository<Role> RoleRepository { get; }
        
        public int Save();
    }
}
