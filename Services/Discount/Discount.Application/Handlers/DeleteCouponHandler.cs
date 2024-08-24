using Discount.Application.Commands;
using Discount.Application.Mappers;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using Grpc.Core;
using LanguageExt;
using LanguageExt.Common;
using MediatorResultPattern.Contract;
using MediatR;

namespace Discount.Application.Handlers;

public class DeleteCouponHandler : IResultRequestHandler<DeleteCouponCommand, bool>
{
    private readonly ICouponRepository _couponRepository;

    public DeleteCouponHandler(ICouponRepository couponRepository)
    {
        _couponRepository = couponRepository;
    }

    public async Task<Result<bool>> Handle(DeleteCouponCommand request, CancellationToken cancellationToken)
    {
        return await _couponRepository.DeleteCoupon(request.CouponId);
    }
}
