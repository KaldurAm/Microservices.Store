#region

using AutoMapper;
using Store.ProductApi.Models;
using Store.ProductApi.Models.Dto;

#endregion

namespace Store.ProductApi;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<ProductDto, Product>();
            config.CreateMap<Product, ProductDto>();
        });

        return mappingConfig;
    }
}