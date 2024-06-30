using Catalog.Application.Mappers;
using Catalog.Application.Response;
using FluentAssertions;

namespace Catalog.Tests.UnitTests;
public class CoreEntitiesToResponseMapper
{
    [Fact]
    public void ProductMapper_MapProductEntityToProductResponse_ShouldReturnProductResponseWithSameValues()
    {
        // Arrange
        var productEntity = new Product
        {
            Id = "1",
            Name = "Sample Product",
            Price = 100.0m,
            Summary = "A brief summary of the product",
            Description = "Detailed description of the product",
            ImageFile = "product-image.jpg",
            Brand = new ProductBrand { Id = "1", Name = "Some brand" },
            Type = new ProductType { Id = "1", Name = "Some Type" }
        };

        // Act
        var productResponse = ProductMapper.Mapper.Map<ProductResponse>(productEntity);

        // Assert
        productResponse.Should().NotBeNull();
        productResponse.Should()
            .BeEquivalentTo(productEntity);
    }

    [Fact]
    public void ProductMapper_MapProductBrandToBrandResponse_ShouldReturnBrandResponseWithSameValues()
    {
        // Arrange
        var productType = new ProductType
        {
            Id = "1",
            Name = "Some Type"
        };

        // Act
        var typeResponse = ProductMapper.Mapper.Map<TypeResponse>(productType);

        // Assert
        typeResponse.Should().NotBeNull();
        typeResponse.Should()
            .BeEquivalentTo(productType);
    }

    [Fact]
    public void ProductMapper_MapProducTypeToTypeResponse_ShouldReturnTypeResponseWithSameValues()
    {
        // Arrange
        var productBrand = new ProductBrand
        {
            Id = "1",
            Name = "Some brand"
        };

        // Act
        var brandResponse = ProductMapper.Mapper.Map<BrandResponse>(productBrand);

        // Assert
        brandResponse.Should().NotBeNull();
        brandResponse.Should()
            .BeEquivalentTo(productBrand);
    }
}