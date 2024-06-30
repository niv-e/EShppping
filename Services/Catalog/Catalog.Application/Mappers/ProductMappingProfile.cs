using AutoMapper;
using Catalog.Application.Response;


namespace Catalog.Application.Mappers;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<ProductType, TypeResponse>().ReverseMap();
        CreateMap<Product, ProductResponse>().ReverseMap();
        CreateMap<ProductBrand, BrandResponse>().ReverseMap();
    }
}
