using AutoMapper;
using System.Data;
using ERP.Data.Model;
using ERP.Domain.Dtos;

namespace ERP.API.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<SiteUserDto, SiteUser>().ReverseMap();
            CreateMap<CartDto, Cart>().ReverseMap();
            CreateMap<CartItemDto, CartItem>().ReverseMap();
            CreateMap<CategoryDto, Category>().ReverseMap();
            CreateMap<OrderItemDto, OrderItem>().ReverseMap();
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<StockDto, Stock>().ReverseMap();
            CreateMap<WarehouseDto, Warehouse>().ReverseMap();
            CreateMap<CreateProductDto, Product>().ReverseMap();
            CreateMap<UpdateProductDto, Product>().ReverseMap();

        }
    }
}
