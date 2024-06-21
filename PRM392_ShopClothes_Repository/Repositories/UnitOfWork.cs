using PRM392_ShopClothes_Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace PRM392_ShopClothes_Repository.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyDBContext _context;
        private IGenericRepository<Category> _categoryClubRepo;
        private IGenericRepository<Member> _memberRepo;
        private IGenericRepository<Order> _orderRepo;
        private IGenericRepository<OrderDetail> _orderDetailRepo;
        private IGenericRepository<Provider> _providerRepo;
        private IGenericRepository<Product> _productRepo;
        private IGenericRepository<Role> _roleRepo;
        private IGenericRepository<Cart> _cartRepo;
        private IGenericRepository<CartItem> _cartItemRepo;


        public UnitOfWork(MyDBContext context)
        {
            _context = context;
        }

        

        public IGenericRepository<Category> CategoryRepository
        {
            get { return _categoryClubRepo ??= new GenericRepository<Category>(_context); }
        }
        public IGenericRepository<Member> MemberRepository
        {
            get { return _memberRepo ??= new GenericRepository<Member>(_context); }
        }

        public IGenericRepository<Order> OrderRepository
        {
            get { return _orderRepo ??= new GenericRepository<Order>(_context); }
        }

        public IGenericRepository<OrderDetail> OrderDetailRepository
        {
            get { return _orderDetailRepo ??= new GenericRepository<OrderDetail>(_context); }
        }

        public IGenericRepository<Product> ProductRepository
        {
            get { return _productRepo ??= new GenericRepository<Product>(_context); }
        }

        public IGenericRepository<Provider> ProviderRepository
        {
            get { return _providerRepo ??= new GenericRepository<Provider>(_context); }
        }

        public IGenericRepository<Role> RoleRepository
        {
            get { return _roleRepo ??= new GenericRepository<Role>(_context); }
        }

        public IGenericRepository<Cart> CartRepository
        {
            get { return _cartRepo ??= new GenericRepository<Cart>(_context); }
        }

        public IGenericRepository<CartItem> CartItemRepository
        {
            get { return _cartItemRepo ??= new GenericRepository<CartItem>(_context); }
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

       
    }
}
