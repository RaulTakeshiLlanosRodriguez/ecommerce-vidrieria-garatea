using AutoMapper;
using EcommerceVidrieria.Application.Features.Categories.Vms;
using EcommerceVidrieria.Application.Features.Cities.Vms;
using EcommerceVidrieria.Application.Features.Images.Vms;
using EcommerceVidrieria.Application.Features.Orders.Vms;
using EcommerceVidrieria.Application.Features.Products.Command.CreateProduct;
using EcommerceVidrieria.Application.Features.Products.Command.UpdateProduct;
using EcommerceVidrieria.Application.Features.Products.Vms;
using EcommerceVidrieria.Application.Features.ShoppingCarts.Vms;
using EcommerceVidrieria.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductVm>()
                .ForMember(p => p.CategoryName, x => x.MapFrom(a => a.Category!.NameCategory));
            CreateMap<Image, ImageVm>();
            CreateMap<Category, CategoryVm>();
            CreateMap<City, CityVm>();
            CreateMap<CreateProductCommand, Product>();
            CreateMap<CreateProductImageVm, Image>();
            CreateMap<UpdateProductCommand, Product>();
            CreateMap<ShoppingCart, ShoppingCartVm>()
                .ForMember(p => p.ShoppingCartId, x => x.MapFrom(a => a.ShoppingCartMasterId));
            CreateMap<ShoppingCartItem, ShoppingCartItemVm>();
            CreateMap<ShoppingCartItemVm, ShoppingCartItem>();
            CreateMap<Order, OrderVm>()
                .ForMember(o => o.NameCity, x => x.MapFrom(c => c.City!.NameCity));
            CreateMap<OrderItem, OrderItemVm>();
        }
    }
}
