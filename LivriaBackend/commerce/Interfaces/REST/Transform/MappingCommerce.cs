using AutoMapper;
using LivriaBackend.commerce.Domain.Model.Aggregates;
using LivriaBackend.commerce.Domain.Model.Commands;
using LivriaBackend.commerce.Domain.Model.Entities;
using LivriaBackend.commerce.Domain.Model.ValueObjects;
using LivriaBackend.commerce.Interfaces.REST.Resources;

namespace LivriaBackend.commerce.Interfaces.REST.Transform
{
    public class MappingCommerce : Profile
    {
        public MappingCommerce()
        {
            CreateMap<Book, BookResource>();
            CreateMap<Review, ReviewResource>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserClient != null ? src.UserClient.Display : "Unknown User"));
            CreateMap<CartItem, CartItemResource>();

            CreateMap<Shipping, ShippingResource>();
            CreateMap<OrderItem, OrderItemResource>();
            CreateMap<Order, OrderResource>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("yyyy-MM-ddTHH:mm:ssZ"))); 
            ;
            
            CreateMap<Recommendation, RecommendationResource>();


            CreateMap<CreateBookResource, CreateBookCommand>();
            CreateMap<CreateReviewResource, CreateReviewCommand>();
            CreateMap<CreateCartItemResource, CreateCartItemCommand>();
            CreateMap<UpdateCartItemQuantityResource, UpdateCartItemQuantityCommand>();

            CreateMap<CreateOrderResource, CreateOrderCommand>();
            CreateMap<ShippingResource, Shipping>(); 
        }
    }
}