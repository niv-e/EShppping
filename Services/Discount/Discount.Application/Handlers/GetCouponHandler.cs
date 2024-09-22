using Discount.Application.Mappers;
using Discount.Application.Queries;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using LanguageExt;
using LanguageExt.Common;
using MediatorResultPattern.Contract;


namespace Discount.Application.Handlers;

public class GetCouponHandler : IResultRequestHandler<GetCouponQurey, CouponModel>
{
    private readonly ICouponRepository _couponRepository;

    public GetCouponHandler(ICouponRepository couponRepository)
    {
        _couponRepository = couponRepository;
    }
    public async Task<Result<CouponModel>> Handle(GetCouponQurey request, CancellationToken cancellationToken) =>
     await _couponRepository.GetCoupon(request.ProductName)
        .Map(optionalCoupon => new Result<CouponModel>(DiscountMapper.Mapper.Map<CouponModel>(optionalCoupon)));
}
