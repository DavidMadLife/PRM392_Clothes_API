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

            //CreateMap<RegisterUserRequest, Member>().ReverseMap();
            CreateMap<CartRequest, CartItem>().ReverseMap();
            CreateMap<OrderRequest, Order>().ReverseMap();

            //Response
            //CreateMap<RegisterUserResponse, Member>().ReverseMap();
            CreateMap<Cart, CartResponse>().ReverseMap();
            CreateMap<CartItem, CartItemResponse>().ReverseMap();
            CreateMap<Order, OrderResponse>().ReverseMap();
            CreateMap<OrderDetail, OrderDetaiResponse>().ReverseMap();

            CreateMap<RegisterUserRequest,PRM392_ShopClothes_Repository.Entities.Member>().ReverseMap();
            CreateMap<UpdateUserRequest, PRM392_ShopClothes_Repository.Entities.Member>().ReverseMap();


            //Response
            CreateMap<RegisterUserResponse, PRM392_ShopClothes_Repository.Entities.Member>().ReverseMap();
            CreateMap<UpdateUserResponse, PRM392_ShopClothes_Repository.Entities.Member>().ReverseMap();

        }
    }
}
