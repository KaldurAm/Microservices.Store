namespace Store.Web.Options;

public class IdentityServerOptions
{
    public const string SectionName = "IdentityServerOptions";

    #pragma warning disable CS8618

    public string CookiesTitle { get; set; }
    public string OidcTitle { get; set; }
    public string SecretTitle { get; set; }
    public string StoreTitle { get; set; }
    public string CodeTitle { get; set; }
    public string RoleTitle { get; set; }
    public string NameTitle { get; set; }
    public string BaseUrl { get; set; }

    #pragma warning restore CS8618
}