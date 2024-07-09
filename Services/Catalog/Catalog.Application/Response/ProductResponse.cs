using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Response;

public record ProductResponse
{
    public string? Id { get; init; }
    public required string Name { get; init; }
    public required decimal Price { get; init; }
    public string? Summary { get; init; }
    public string? Description { get; init; }
    public string? ImageFile { get; init; }
    public ProductBrand? Brand { get; init; }
    public ProductType? Type { get; init; }
}
