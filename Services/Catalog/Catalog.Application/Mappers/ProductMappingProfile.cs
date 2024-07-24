﻿using AutoMapper;
using Catalog.Application.Commands;
using Catalog.Application.Queries;
using Catalog.Application.Response;


namespace Catalog.Application.Mappers;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<ProductType, TypeResponse>().ReverseMap();
        CreateMap<Product, ProductResponse>().ReverseMap();
        CreateMap<Product, CreateProductCommand>().ReverseMap();
        CreateMap<ProductBrand, BrandResponse>().ReverseMap();
        CreateMap<GetProductsByQuery, ProductsFilter>().ReverseMap();
    }
}
