#region

using Microsoft.Extensions.Options;
using Store.Web.Models;
using Store.Web.Options;
using Store.Web.Services.IServices;

#endregion

namespace Store.Web.Services;

public class ProductService : IProductService
{
    private readonly IBaseService _baseService;
    private readonly ProductApiOptions _productApiOptions;

    public ProductService(IBaseService baseService, IOptions<ProductApiOptions> productApiOptions)
    {
        _baseService = baseService;
        _productApiOptions = productApiOptions.Value;
    }

    /// <inheritdoc />
    public async Task<T> GetAsync<T>()
    {
        var response = await _baseService.SendAsync<T>(new ApiRequest
        {
            ApiType = ApiType.GET,
            Url = _productApiOptions.BaseUrl + "/api/products/",
            AccessToken = "",
        });

        return response;
    }

    /// <inheritdoc />
    public async Task<T> GetByIdAsync<T>(int id)
    {
        var response = await _baseService.SendAsync<T>(new ApiRequest
        {
            ApiType = ApiType.GET,
            Url = _productApiOptions.BaseUrl + "/api/products/" + id,
            AccessToken = "",
        });

        return response;
    }

    /// <inheritdoc />
    public async Task<T> CreateAsync<T>(ProductDto product)
    {
        var response = await _baseService.SendAsync<T>(new ApiRequest
        {
            ApiType = ApiType.POST,
            Data = product,
            Url = _productApiOptions.BaseUrl + "/api/products/create",
            AccessToken = "",
        });

        return response;
    }

    /// <inheritdoc />
    public async Task<T> UpdateAsync<T>(ProductDto product)
    {
        var response = await _baseService.SendAsync<T>(new ApiRequest
        {
            ApiType = ApiType.PUT,
            Data = product,
            Url = _productApiOptions.BaseUrl + "/api/products/update",
            AccessToken = "",
        });

        return response;
    }

    /// <inheritdoc />
    public async Task<T> DeleteAsync<T>(int id)
    {
        var response = await _baseService.SendAsync<T>(new ApiRequest
        {
            ApiType = ApiType.DELETE,
            Url = _productApiOptions.BaseUrl + "/api/products/delete/" + id,
            AccessToken = "",
        });

        return response;
    }
}