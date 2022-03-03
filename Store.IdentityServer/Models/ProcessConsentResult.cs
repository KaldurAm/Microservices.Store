#region

using Duende.IdentityServer.Models;
using Store.IdentityServer.ViewModels;

#endregion

namespace Store.IdentityServer.Models;

public class ProcessConsentResult
{
    public bool IsRedirect
    {
        get
        {
            return RedirectUri != null;
        }
    }

    public string RedirectUri { get; set; }
    public Client Client { get; set; }

    public bool ShowView
    {
        get
        {
            return ViewModel != null;
        }
    }

    public ConsentViewModel ViewModel { get; set; }

    public bool HasValidationError
    {
        get
        {
            return ValidationError != null;
        }
    }

    public string ValidationError { get; set; }
}