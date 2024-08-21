using CSharpFunctionalExtensions;
using Discount.Application.Commands;
using Discount.Application.Errors;
using Discount.Application.Mappers;
using Discount.Application.Queries;
using Discount.Application.Responses;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using Grpc.Core;
using MediatorResultPattern.Contract;
using MediatR;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Discount.Application.Handlers;

public class CreateCouponHandler : IResultRequestHandler<CreateCouponCommand, CouponResponse>
{
    private readonly ICouponRepository _couponRepository;

    public CreateCouponHandler(ICouponRepository couponRepository)
    {
        _couponRepository = couponRepository;
    }

    public async Task<Result<CouponResponse, InternalError>> Handle(CreateCouponCommand request, CancellationToken cancellationToken)
    {
        var coupon = DiscountMapper.Mapper.Map<Coupon>(request);

        var createResult = await _couponRepository.CreateCoupon(coupon)
              ? Result.Success<Coupon, InternalError>(coupon)
              : Result.Failure<Coupon, InternalError>(new UnknownError($"Failed to create new coupon with the following details: {coupon}"));

        return createResult.Map(coupon => DiscountMapper.Mapper.Map<CouponResponse>(coupon));
    }
}
