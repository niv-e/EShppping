﻿using Discount.Grpc.Protos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Application.Commands;

public record UpdateCouponCommand : IRequest<CouponModel>
{
    public string? Id { get; set; }
    public string? ProductName { get; set; }
    public string? Description { get; set; }
    public int Amount { get; set; }
    public bool IsActive { get; set; }

}
