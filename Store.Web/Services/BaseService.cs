#region

using System.Text;
using Newtonsoft.Json;
using Store.Web.Models;
using Store.Web.Options;
using Store.Web.Services.IServices;

#endregion

namespace Store.Web.Services;

public class BaseService : IBaseService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public BaseService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        Response = new ResponseDto();
    }

    /// <inheritdoc />
    public ResponseDto Response { get; set; }

    /// <inheritdoc />
    public async Task<T> SendAsync<T>(ApiRequest request)
    {
        try
        {
            var client = _httpClientFactory.CreateClient("ProductApi");
            HttpRequestMessage message = new();
            message.Headers.Add("Accept", "appication/json");
            message.RequestUri = new Uri(request.Url);
            client.DefaultRequestHeaders.Clear();

            message.Content = new StringContent(JsonConvert.SerializeObject(request.Data), Encoding.UTF8,
                "application/json");

            message.Method = request.ApiType switch
            {
                ApiType.POST => HttpMethod.Post,
                ApiType.PUT => HttpMethod.Put,
                ApiType.DELETE => HttpMethod.Delete,
                ApiType.GET => HttpMethod.Get,
                _ => message.Method,
            };

            var response = await client.SendAsync(message);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(content);

            return result;
        }
        catch (Exception ex)
        {
            ResponseDto dto = new()
            {
                DisplayMessage = "Error",
                ErrorMessages = new List<string> { Convert.ToString(ex.Message) },
                IsSuccess = false,
            };

            var res = JsonConvert.SerializeObject(dto);
            var response = JsonConvert.DeserializeObject<T>(res);

            return response;
        }
    }

    /// <inheritdoc />
    public void Dispose()
    {
        GC.SuppressFinalize(true);
    }
}