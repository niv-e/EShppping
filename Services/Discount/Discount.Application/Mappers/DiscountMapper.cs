﻿using AutoMapper;

namespace Discount.Application.Mappers;


public static class DiscountMapper
{
    private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.ShouldMapProperty = p => p.GetMethod!.IsPublic || p.GetMethod.IsAssembly;
            cfg.ShouldUseConstructor = constructor => constructor.IsPublic;
            cfg.AddProfile<DiscountMappingProfile>();
        });
        var mapper = config.CreateMapper();
        return mapper;
    });

    public static IMapper Mapper => Lazy.Value;
}
