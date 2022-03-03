#region

using Microsoft.AspNetCore.Identity;

#endregion

namespace Store.IdentityServer.Models;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}