using Discount.Application.Mappers;
using Discount.Application.Responses;
using Discount.Core.Entities;

namespace Discount.Tests.UnitTests;

public class AutoMapperTests
{
    [Fact]
    public void DiscountMappingProfile_MapCouponToCouponResponse_ShouldReturnCouponResponseWithSameValues()
    {
        var coupon = new Coupon
        {
            Id = "123",
            ProductName = "Product A",
            Description = "Description of Product A",
            Amount = 100, 
            IsActive = true
        };

        var couponResponse = DiscountMapper.Mapper.Map<CouponResponse>(coupon);

        Assert.NotNull(couponResponse);
        Assert.NotNull(couponResponse.Coupon);
        Assert.Equal(coupon.Id, couponResponse.Coupon.Id);
        Assert.Equal(coupon.ProductName, couponResponse.Coupon.ProductName);
        Assert.Equal(coupon.Description, couponResponse.Coupon.Description);
        Assert.Equal(coupon.Amount, couponResponse.Coupon.Amount);
    }
}