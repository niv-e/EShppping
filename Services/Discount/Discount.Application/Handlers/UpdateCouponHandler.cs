using Discount.Application.Commands;
using Discount.Application.Mappers;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using Grpc.Core;
using MediatR;

namespace Discount.Application.Handlers;

public class UpdateCouponHandler : IRequestHandler<UpdateCouponCommand, CouponModel>
{
    private readonly ICouponRepository _couponRepository;

    public UpdateCouponHandler(ICouponRepository couponRepository)
    {
        _couponRepository = couponRepository;
    }

    public async Task<CouponModel> Handle(UpdateCouponCommand request, CancellationToken cancellationToken)
    {
        var coupon = DiscountMapper.Mapper.Map<Coupon>(request);
        var updateResults = await _couponRepository.UpdateCoupon(coupon);
        if (coupon == null)
        {
            throw new RpcException(new Status(StatusCode.Internal,
                $"Failed to update new coupon with the following details: {coupon}"));
        }
        var couponModel = DiscountMapper.Mapper.Map<CouponModel>(coupon);
        return couponModel;

    }
}
