#region

using Microsoft.Extensions.Options;
using Store.Web.Options;
using Store.Web.Services;
using Store.Web.Services.IServices;

#endregion

#pragma warning disable ASP0000

var builder = WebApplication.CreateBuilder(args);

// Add web dependencies
builder.Services.AddControllersWithViews();

// Configure static options
builder.Services.Configure<ProductApiOptions>(builder.Configuration.GetSection(ProductApiOptions.SectionName));
builder.Services.Configure<IdentityServerOptions>(builder.Configuration.GetSection(IdentityServerOptions.SectionName));

// Configure OpenId connect and identity
var identityServerOptions
    = builder.Services.BuildServiceProvider().GetRequiredService<IOptions<IdentityServerOptions>>().Value;

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = identityServerOptions.CookiesTitle;
    options.DefaultChallengeScheme = identityServerOptions.OidcTitle;
}).AddCookie(identityServerOptions.CookiesTitle, c => c.ExpireTimeSpan = TimeSpan.FromMinutes(30)).AddOpenIdConnect(
    identityServerOptions.OidcTitle, options =>
    {
        options.Authority = identityServerOptions.BaseUrl;
        options.GetClaimsFromUserInfoEndpoint = true;
        options.ClientId = identityServerOptions.StoreTitle;
        options.ClientSecret = identityServerOptions.SecretTitle;
        options.ResponseType = identityServerOptions.CodeTitle;
        options.TokenValidationParameters.NameClaimType = identityServerOptions.NameTitle;
        options.TokenValidationParameters.RoleClaimType = identityServerOptions.RoleTitle;
        options.Scope.Add(identityServerOptions.StoreTitle);
        options.SaveTokens = true;
    });

// Add services to the DI container.
builder.Services.AddHttpClient<IProductService, ProductService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IBaseService, BaseService>();

#pragma warning restore ASP0000

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

app.Run();