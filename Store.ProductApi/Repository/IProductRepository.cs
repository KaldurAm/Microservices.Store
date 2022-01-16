using Store.ProductApi.Models.Dto;

namespace Store.ProductApi.Repository;

public interface IProductRepository
{
    Task<IEnumerable<ProductDto>> Get();
    Task<ProductDto> GetById(int id);
    Task<ProductDto> Create(ProductDto product);
    Task<ProductDto> Update(ProductDto product);
    Task<bool> Delete(int id);
}