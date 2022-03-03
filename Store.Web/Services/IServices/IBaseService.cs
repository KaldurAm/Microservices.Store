#region

using Store.Web.Models;

#endregion

namespace Store.Web.Services.IServices;

public interface IBaseService : IDisposable
{
    ResponseDto Response { get; set; }
    Task<T> SendAsync<T>(ApiRequest request);
}