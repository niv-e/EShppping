using Discount.Application.Commands;
using Discount.Application.Mappers;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using LanguageExt;
using LanguageExt.Common;
using MediatorResultPattern.Contract;
using static LanguageExt.Prelude;

namespace Discount.Application.Handlers;

public class CreateCouponHandler : IResultRequestHandler<CreateCouponCommand, CouponModel>
{
    private readonly ICouponRepository _couponRepository;

    public CreateCouponHandler(ICouponRepository couponRepository)
    {
        _couponRepository = couponRepository;
    }

    public async Task<Result<CouponModel>> Handle(CreateCouponCommand request, CancellationToken cancellationToken)
    {
        return await _couponRepository.CreateCoupon(DiscountMapper.Mapper.Map<Coupon>(request))
            .Map(created => created
                ? new Result<CouponModel>(DiscountMapper.Mapper.Map<CouponModel>(request))
                : new Result<CouponModel>(new ArgumentException($"Failed to create coupon with the following details: {request}")));
    }
}
