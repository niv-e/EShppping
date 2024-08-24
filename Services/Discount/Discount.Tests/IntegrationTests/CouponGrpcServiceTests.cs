using Discount.Core.Entities;
using Discount.Grpc.Protos;
using Discount.Tests.IntegrationTests;
using Discount.Tests.IntegrationTests.Helpers;
using FluentAssertions;
using Grpc.Core;
using Xunit.Abstractions;

namespace Discount.Tests.IntegrationTets;

public class CouponGrpcServiceTests: IntegrationTestBase
{
    public CouponGrpcServiceTests(GrpcTestFixture<Program> fixture, ITestOutputHelper outputHelper)
    : base(fixture, outputHelper)
    {
    }

    [Fact]
    public async Task GetCoupon()
    {
        // Arrange
        var client = new CouponGrpcService.CouponGrpcServiceClient(Channel);
        var couponeModel = new CouponModel
        {
            Id = "123",
            ProductName = "Product A",
            Description = "Description of Product A",
            Amount = 100
        };
        // Act
        var response = await client.CreateCouponAsync(new CreateCouponRequest
        {
            Coupon = couponeModel
        });

        // Assert
        response.Id.Should().Be(couponeModel.Id);
        response.ProductName.Should().Be(couponeModel.ProductName);
        response.Description.Should().Be(couponeModel.Description);
        response.Amount.Should().Be(couponeModel.Amount);
    }
}