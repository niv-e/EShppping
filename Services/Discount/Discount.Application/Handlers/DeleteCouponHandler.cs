using Discount.Application.Commands;
using Discount.Application.Mappers;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using Grpc.Core;
using MediatR;

namespace Discount.Application.Handlers;

public class DeleteCouponHandler : IRequestHandler<DeleteCouponCommand, bool>
{
    private readonly ICouponRepository _couponRepository;

    public DeleteCouponHandler(ICouponRepository couponRepository)
    {
        _couponRepository = couponRepository;
    }

    public async Task<bool> Handle(DeleteCouponCommand request, CancellationToken cancellationToken)
    {
        var deleteResult = await _couponRepository.DeleteCoupon(request.CouponId);
        if (deleteResult is false)
        {
            throw new RpcException(new Status(StatusCode.NotFound,
                $"Could not delete coupon with id: {request.CouponId}"));
        }
        return deleteResult;
    }
}
