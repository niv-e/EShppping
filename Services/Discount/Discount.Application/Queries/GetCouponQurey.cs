using Discount.Grpc.Protos;
using MediatorResultPattern.Contract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Application.Queries;

public record GetCouponQurey(string ProductName) : IResultRequest<CouponModel>;