using Discount.Grpc.Protos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Application.Queries;

public record GetCouponQurey(string ProductName) : IRequest<CouponModel> { }
