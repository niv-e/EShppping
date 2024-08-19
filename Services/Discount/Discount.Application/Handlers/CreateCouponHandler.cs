using Discount.Application.Commands;
using Discount.Application.Mappers;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using Grpc.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Application.Handlers;

public class CreateCouponHandler : IRequestHandler<CreateCouponCommand, CouponModel>
{
    private readonly ICouponRepository _couponRepository;

    public CreateCouponHandler(ICouponRepository couponRepository)
    {
        _couponRepository = couponRepository;
    }

    public async Task<CouponModel> Handle(CreateCouponCommand request, CancellationToken cancellationToken)
    {
        var coupon = DiscountMapper.Mapper.Map<Coupon>(request);
        var createResult = await _couponRepository.CreateCoupon(coupon);
        if(createResult is false)
        {
            throw new RpcException(new Status(StatusCode.Internal, $"Failed to create new coupon with the following details: {coupon}"));
        }
        var couponModel = DiscountMapper.Mapper.Map<CouponModel>(coupon);
        return couponModel;
    }
}
