
using MediatorResultPattern.Contract;
using MediatR;

namespace Discount.Application.Commands;

public record DeleteCouponCommand(string CouponId) : IResultRequest<bool> { }
