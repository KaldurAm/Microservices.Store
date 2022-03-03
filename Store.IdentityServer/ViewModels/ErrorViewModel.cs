#region

using Duende.IdentityServer.Models;

#endregion

namespace Store.IdentityServer.ViewModels;

public class ErrorViewModel
{
    public ErrorViewModel() { }

    public ErrorViewModel(string requestId, string error)
    {
        RequestId = requestId;
        Error = new ErrorMessage { Error = error };
    }

    public string RequestId { get; set; }

    public ErrorMessage Error { get; set; }
}