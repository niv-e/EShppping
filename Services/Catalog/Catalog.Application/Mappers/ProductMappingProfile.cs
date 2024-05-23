using AutoMapper;
using Catalog.Application.Response;


namespace Catalog.Application.Mappers;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<ProductBrand,BrandResponse>().ReverseMap();
    }
}
