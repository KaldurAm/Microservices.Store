#region

using Duende.IdentityServer;
using Duende.IdentityServer.Models;

#endregion

namespace IdentityServerHost.Options;

public static class Roles
{
    public const string Admin = "Admin";
    public const string Customer = "Customer";

    public static IEnumerable<IdentityResource> IdentityResources
    {
        get
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile(),
            };
        }
    }

    public static IEnumerable<ApiScope> ApiScopes
    {
        get
        {
            return new List<ApiScope>
            {
                new("store", "Store Server"),
                new("read", "Read your data."),
                new("write", "Write your data."),
                new("delete", "Delete your data."),
            };
        }
    }

    public static IEnumerable<Client> Clients
    {
        get
        {
            return new List<Client>
            {
                new()
                {
                    ClientId = "client",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "read", "write", "profile" },
                },
                new()
                {
                    ClientId = "store",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "https://localhost:7098/signing-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:7098/signout-callback-oidc" },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "store",
                    },
                },
            };
        }
    }
}