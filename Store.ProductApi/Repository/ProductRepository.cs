#region

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Store.ProductApi.Database;
using Store.ProductApi.Models;
using Store.ProductApi.Models.Dto;

#endregion

namespace Store.ProductApi.Repository;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ProductRepository> _logger;
    private readonly IMapper _mapper;

    public ProductRepository(ApplicationDbContext context, IMapper mapper, ILogger<ProductRepository> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <inheritdoc />
    public async Task<IEnumerable<ProductDto>> Get()
    {
        _logger.LogInformation(nameof(Get) + " method started");
        var products = await _context.Products.ToListAsync();

        return _mapper.Map<List<ProductDto>>(products);
    }

    /// <inheritdoc />
    public async Task<ProductDto> GetById(int id)
    {
        var product = await _context.Products.FindAsync(id);

        return _mapper.Map<ProductDto>(product);
    }

    /// <inheritdoc />
    public async Task<ProductDto> Create(ProductDto? product)
    {
        if (product is null)
            throw new ArgumentNullException(nameof(product));

        var domainProduct = _mapper.Map<Product>(product);
        var createdProduct = await _context.Products.AddAsync(domainProduct);
        await _context.SaveChangesAsync();

        return _mapper.Map<ProductDto>(createdProduct.Entity);
    }

    /// <inheritdoc />
    public async Task<ProductDto> Update(ProductDto product)
    {
        if (product is null)
            throw new ArgumentNullException(nameof(product));

        var domainProduct = _mapper.Map<Product>(product);
        var updatedProduct = _context.Products.Update(domainProduct);
        await _context.SaveChangesAsync();

        return _mapper.Map<ProductDto>(updatedProduct.Entity);
    }

    /// <inheritdoc />
    public async Task<bool> Delete(int id)
    {
        _logger.LogInformation("method delete started");

        if (id == 0)
            throw new ArgumentException(nameof(id));

        var product = await _context.Products.FindAsync(id);

        if (product is null)
        {
            _logger.LogInformation("product is null");

            throw new NullReferenceException(nameof(product));
        }

        try
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);

            return false;
        }
    }
}