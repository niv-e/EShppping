﻿using Discount.Core.Entities;
using Discount.Grpc.Protos;
using MediatR;

namespace Discount.Application.Commands;

public record CreateCouponCommand : IRequest<CouponModel>
{
    public string? ProductName { get; set; }
    public string? Description { get; set; }
    public int Amount { get; set; }

}
