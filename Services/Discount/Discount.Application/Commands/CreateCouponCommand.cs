using Discount.Application.Responses;
using Discount.Core.Entities;
using Discount.Grpc.Protos;
using MediatorResultPattern.Contract;
using MediatR;

namespace Discount.Application.Commands;

public record CreateCouponCommand : IResultRequest<CouponResponse>
{
    public string? ProductName { get; set; }
    public string? Description { get; set; }
    public int Amount { get; set; }

}
