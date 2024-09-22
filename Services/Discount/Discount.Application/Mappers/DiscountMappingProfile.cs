using AutoMapper;
using Discount.Application.Commands;
using Discount.Application.Queries;
using Discount.Application.Responses;
using Discount.Core.Entities;
using Discount.Grpc.Protos;

namespace Discount.Application.Mappers;

public class DiscountMappingProfile : Profile
{
    public DiscountMappingProfile()
    {
        CreateMap<Coupon, CouponModel>().ReverseMap();
        CreateMap<Coupon, CouponResponse>()
            .ForMember(dest => dest.Coupon, opt => opt.MapFrom(src => src));
        CreateMap<CreateCouponCommand, Coupon>().ReverseMap();
        CreateMap<UpdateCouponCommand, Coupon>().ReverseMap();
        CreateMap<DeleteCouponCommand, Coupon>()
            .ForMember(coupon => coupon.Id, opt => opt.MapFrom(coupon => coupon.CouponId))
            .ReverseMap();

    }
}
