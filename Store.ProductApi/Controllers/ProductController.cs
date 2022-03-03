#region

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.ProductApi.Models.Dto;
using Store.ProductApi.Repository;

#endregion

namespace Store.ProductApi.Controllers;

[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IProductRepository _productRepository;
    private readonly ResponseDto _response;

    public ProductController(IProductRepository productRepository, ILogger<ProductController> logger)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _response = new ResponseDto();
    }

    [HttpGet]
    public async Task<object> Get()
    {
        _logger.LogDebug(nameof(Get) + " start");

        try
        {
            var products = await _productRepository.Get();
            _response.Result = products;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string> { ex.ToString() };
        }

        _logger.LogDebug(nameof(Get) + " finish");

        return _response;
    }

    [HttpGet("{id}")]
    public async Task<object> Get(int id)
    {
        _logger.LogDebug(nameof(Get) + " by id start");

        try
        {
            var products = await _productRepository.GetById(id);
            _response.Result = products;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string> { ex.ToString() };
        }

        _logger.LogDebug(nameof(Get) + " by id finish");

        return _response;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("create")]
    public async Task<object> Create([FromBody] ProductDto product)
    {
        _logger.LogDebug(nameof(Create) + " start");

        try
        {
            var products = await _productRepository.Create(product);
            _response.Result = products;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string> { ex.ToString() };
        }

        _logger.LogDebug(nameof(Create) + " finish");

        return _response;
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("update")]
    public async Task<object> Update([FromBody] ProductDto product)
    {
        _logger.LogDebug(nameof(Update) + " start");

        try
        {
            var products = await _productRepository.Update(product);
            _response.Result = products;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string> { ex.ToString() };
        }

        _logger.LogDebug(nameof(Update) + " finish");

        return _response;
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("delete/{id:int}")]
    public async Task<object> Delete(int id)
    {
        _logger.LogDebug(nameof(Delete) + " start");

        try
        {
            var deleted = await _productRepository.Delete(id);
            _response.Result = deleted;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string> { ex.ToString() };
        }

        _logger.LogDebug(nameof(Delete) + " finish");

        return _response;
    }
}