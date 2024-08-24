
using Discount.Application.Commands;
using Discount.Application.Mappers;
using Discount.Core.Entities;
using Discount.Grpc.Protos;
using Grpc.Core;
using LanguageExt.SomeHelp;
using MediatR;

namespace Discount.API.Services;

public class CouponService : CouponGrpcService.CouponGrpcServiceBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<CouponService> _logger;
    public CouponService(IMediator mediator, ILogger<CouponService> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public async override Task<CouponModel> CreateCoupon(CreateCouponRequest request, ServerCallContext context) =>
        (await _mediator.Send(DiscountMapper.Mapper.Map<CreateCouponCommand>(request.Coupon)))
            .Match(
            coupon =>
            {
                _logger.LogInformation("Coupon: @{Coupon} created successfully", coupon);
                return coupon;
            },
            (ex) =>
            {
                _logger.LogError(ex,"Failed to create coupon: @{Coupon}", request.Coupon);
                throw new RpcException(new Status(StatusCode.Unknown, $"Failed to create coupon with parameters: {request.Coupon}"));
            });
    

    public async override Task<DeleteCouponResponse> DeleteCoupon(DeleteCouponRequest request, ServerCallContext context)
    {
        return (await _mediator.Send(DiscountMapper.Mapper.Map<DeleteCouponCommand>(request.CouponId)))
            .Match(
                isDeleted =>
                {
            _logger.Log(isDeleted ? LogLevel.Information : LogLevel.Error, 
                        isDeleted ? "Coupon with id: {CouponId} deleted successfully" : "Failed to delete coupon with id: {CouponId}", 
                        request.CouponId);
                    return new DeleteCouponResponse
                    {
                        Success = isDeleted
                    };
                },
                (ex) =>
                {
                    _logger.LogError("Failed to delete coupon with id: @{CouponId}", request.CouponId);
                    throw new RpcException(new Status(StatusCode.Unknown, $"Failed to delete coupon with id: {request.CouponId}"));
                });
    }

    public override Task<CouponModel> GetCoupon(GetCouponRequest request, ServerCallContext context)
    {
        return base.GetCoupon(request, context);
    }

    public override Task<CouponModel> UpdateCoupon(UpdateCouponRequest request, ServerCallContext context)
    {
        return base.UpdateCoupon(request, context);
    }
}
