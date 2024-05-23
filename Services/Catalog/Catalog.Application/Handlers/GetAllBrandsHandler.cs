using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Handlers;

public class GetAllBrandsHandler : IRequestHandler<GetAllBrandsQuery, IList<BrandResponse>>
{
    private readonly IBrandRepository _brandsRepostory;

    public GetAllBrandsHandler(IBrandRepository brandsRepostory)
    {
        _brandsRepostory = brandsRepostory;
    }

    public async Task<IList<BrandResponse>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
    {
        var brands = await _brandsRepostory.GetAllBrands();
        var brandsResponse = ProductMapper.Mapper.Map<IList<ProductBrand>,IList<BrandResponse>>(brands.ToList());
        return brandsResponse;
    }
}
