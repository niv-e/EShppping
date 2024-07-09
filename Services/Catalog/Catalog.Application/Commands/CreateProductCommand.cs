using Catalog.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Commands;

public class CreateProductCommand : IRequest<ProductResponse>
{
    public required string Name { get; init; }
    public required decimal Price { get; init; }
    public string? Summary { get; set; }
    public string? Description { get; set; }
    public string? ImageFile { get; set; }
    public ProductBrand? Brand { get; set; }
    public ProductType? Type { get; set; }
}
