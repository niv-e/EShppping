using Catalog.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Queries;

public class GetAllProductsQuery : IRequest<IList<ProductResponse>>
{
    public int PageNumber { get; init; } = 0;
    public int PageSize { get; init; } = 20;

}
