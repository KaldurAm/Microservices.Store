namespace Store.Web.Options;

public class ProductApiOptions
{
    public const string SectionName = "ProductApiOptions";

    #pragma warning disable CS8618

    public string BaseUrl { get; set; }

    #pragma warning restore CS8618
}