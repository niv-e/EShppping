using Discount.Application.Mappers;
using Discount.Application.Queries;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using Grpc.Core;
using MediatR;


namespace Discount.Application.Handlers;

public class GetCouponHandler : IRequestHandler<GetCouponQurey, CouponModel>
{
    private readonly ICouponRepository _couponRepository;

    public GetCouponHandler(ICouponRepository couponRepository)
    {
        _couponRepository = couponRepository;
    }
    public async Task<CouponModel> Handle(GetCouponQurey request, CancellationToken cancellationToken)
    {
        var coupon = await _couponRepository.GetCoupon(request.ProductName);
        if (coupon == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound,
                $"Coupon with the product name: {request.ProductName} not found"));
        }
        var couponModel = DiscountMapper.Mapper.Map<CouponModel>(coupon);
        return couponModel;
    }
}
