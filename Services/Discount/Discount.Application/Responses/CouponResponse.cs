using Discount.Grpc.Protos;


namespace Discount.Application.Responses;

public record CouponResponse
{
    public CouponModel? Coupon { get; set; }
}
