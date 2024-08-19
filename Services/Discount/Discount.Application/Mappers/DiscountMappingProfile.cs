using AutoMapper;
using Discount.Application.Commands;
using Discount.Core.Entities;
using Discount.Grpc.Protos;

namespace Discount.Application.Mappers;

public class DiscountMappingProfile : Profile
{
    public DiscountMappingProfile()
    {
        CreateMap<Coupon, CouponModel>().ReverseMap();
        CreateMap<CreateCouponCommand, Coupon>().ReverseMap();
        CreateMap<UpdateCouponCommand, Coupon>().ReverseMap();
        CreateMap<DeleteCouponCommand, Coupon>().ReverseMap();
    }
}
