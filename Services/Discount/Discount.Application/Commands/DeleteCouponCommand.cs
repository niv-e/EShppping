
using MediatR;

namespace Discount.Application.Commands;

public record DeleteCouponCommand(string CouponId) : IRequest<bool> { }
