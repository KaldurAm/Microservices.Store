#region

using Store.IdentityServer.Providers;

#endregion

namespace Store.IdentityServer.Models;

public class LoginViewModel : LoginInputModel
{
    public bool AllowRememberLogin { get; set; } = true;
    public bool EnableLocalLogin { get; set; } = true;

    public IEnumerable<ExternalProvider> ExternalProviders { get; set; } = Enumerable.Empty<ExternalProvider>();

    public IEnumerable<ExternalProvider> VisibleExternalProviders
    {
        get
        {
            return ExternalProviders.Where(x => !string.IsNullOrWhiteSpace(x.DisplayName));
        }
    }

    public bool IsExternalLoginOnly
    {
        get
        {
            return EnableLocalLogin == false && ExternalProviders?.Count() == 1;
        }
    }

    public string ExternalLoginScheme
    {
        get
        {
            return IsExternalLoginOnly ? ExternalProviders?.SingleOrDefault()?.AuthenticationScheme : null;
        }
    }
}