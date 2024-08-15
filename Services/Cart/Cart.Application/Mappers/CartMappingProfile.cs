using AutoMapper;
using AutoMapper.Features;
using Cart.Application.Responses;
using Cart.Core.Entities;
using Cart.Application.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Application.Mappers;
public  class CartMappingProfile : Profile
{
    public CartMappingProfile()
    {
        CreateMap<ShoppingCart, ShoppingCartResponse>()
            .ForMember(response => response.TotalPrice, opt =>
            {
                opt.MapFrom(cart => TotalPriceCalculator.Calculate(cart));
            });
        CreateMap<ShoppingCartResponse, ShoppingCart>();
        CreateMap<ShoppingCartItem, ShoppingCartItemResponse>().ReverseMap();

    }
}
