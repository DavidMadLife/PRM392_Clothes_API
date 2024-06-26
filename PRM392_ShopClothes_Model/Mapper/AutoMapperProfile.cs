using AutoMapper;
using AutoMapper.Execution;
using PRM392_ShopClothes_Model.Model.Request;
using PRM392_ShopClothes_Model.Model.Response;
using PRM392_ShopClothes_Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PRM392_ShopClothes_Model.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Request
            CreateMap<RegisterUserRequest,PRM392_ShopClothes_Repository.Entities.Member>().ReverseMap();
            CreateMap<UpdateUserRequest, PRM392_ShopClothes_Repository.Entities.Member>().ReverseMap();
            CreateMap<CartRequest, CartItem>().ReverseMap();
            CreateMap<OrderRequest, Order>().ReverseMap();
            CreateMap<ProductRequest, Product>().ReverseMap();
            CreateMap<ProviderRequest, Provider>().ReverseMap();
            CreateMap<CategoryRequest, Category>().ReverseMap();


            //Response
<<<<<<< HEAD
            //CreateMap<RegisterUserResponse, Member>().ReverseMap();
            CreateMap<Cart, CartResponse>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.CartItems))
                .ReverseMap();
            CreateMap<CartItem, CartItemResponse>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.Product.UnitPrice))
                .ReverseMap();
            CreateMap<Order, OrderResponse>().ReverseMap();
            CreateMap<OrderDetail, OrderDetaiResponse>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
                .ReverseMap();

            CreateMap<RegisterUserRequest,PRM392_ShopClothes_Repository.Entities.Member>().ReverseMap();
            CreateMap<UpdateUserRequest, PRM392_ShopClothes_Repository.Entities.Member>().ReverseMap();


            //Response
=======
            CreateMap<Cart, CartResponse>().ReverseMap();
            CreateMap<CartItem, CartItemResponse>().ReverseMap();
            CreateMap<Order, OrderResponse>().ReverseMap();
            CreateMap<OrderDetail, OrderDetaiResponse>().ReverseMap();
>>>>>>> 4b918001dd17e39fafe46b417de03c72f2f3151f
            CreateMap<RegisterUserResponse, PRM392_ShopClothes_Repository.Entities.Member>().ReverseMap();
            CreateMap<UpdateUserResponse, PRM392_ShopClothes_Repository.Entities.Member>().ReverseMap();
            CreateMap<ProductResponse, Product>().ReverseMap();
            CreateMap<ProviderResponse, Provider>().ReverseMap();
            CreateMap<CategoryResponse, Category>().ReverseMap();

        }
    }
}
