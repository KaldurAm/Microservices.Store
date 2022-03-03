#region

using Store.IdentityServer.Models;

#endregion

namespace Store.IdentityServer.ViewModels;

public class LogoutViewModel : LogoutInputModel
{
    public bool ShowLogoutPrompt { get; set; } = true;
}