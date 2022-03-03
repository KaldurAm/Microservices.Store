#region

using Store.Web.Models;

#endregion

namespace Store.Web.Services.IServices;

public interface IProductService
{
    Task<T> GetAsync<T>();
    Task<T> GetByIdAsync<T>(int id);
    Task<T> CreateAsync<T>(ProductDto product);
    Task<T> UpdateAsync<T>(ProductDto product);
    Task<T> DeleteAsync<T>(int id);
}