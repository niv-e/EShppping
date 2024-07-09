namespace Catalog.Application.Response;

public record BrandResponse
{
    public string? Id { get; init; }
    public string? Name { get; init; }
}