using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.ProductApi.Models.Dto;
using Store.ProductApi.Repository;

namespace Store.ProductApi.Controllers;

[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly ResponseDto _response;
    private readonly IProductRepository _productRepository;

    public ProductController(IProductRepository productRepository, 
        ILogger<ProductController> logger)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _response = new ResponseDto();
    }

    [Authorize]
    [HttpGet]
    public async Task<object> Get()
    {
        try
        {
            var products = await _productRepository.Get();
            _response.Result = products;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }

        return _response;
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<object> Get(int id)
    {
        try
        {
            var products = await _productRepository.GetById(id);
            _response.Result = products;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }

        return _response;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("create")]
    public async Task<object> Create([FromBody] ProductDto product)
    {
        try
        {
            var products = await _productRepository.Create(product);
            _response.Result = products;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }

        return _response;
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPut("update")]
    public async Task<object> Update([FromBody] ProductDto product)
    {
        try
        {
            var products = await _productRepository.Update(product);
            _response.Result = products;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }

        return _response;
    }
    
    [Authorize(Roles = "Admin")]
    [HttpDelete("delete/{id}")]
    public async Task<object> Delete(int id)
    {
        _logger.LogInformation(nameof(Delete) + " method started");
        try
        {
            var deleted = await _productRepository.Delete(id);
            _response.Result = deleted;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }

        _logger.LogInformation(nameof(Delete) + " finish with result " + _response.Result);
        return _response;
    }
}