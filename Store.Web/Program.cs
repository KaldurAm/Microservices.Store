using Store.Web.Options;
using Store.Web.Services;
using Store.Web.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure OpenId connect and identity
builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = "Cookies";
        options.DefaultChallengeScheme = "oidc";
    })
    .AddCookie("Cookies", c => c.ExpireTimeSpan = TimeSpan.FromMinutes(30))
    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority = builder.Configuration["IdentityServerOptions:BaseUrl"];
        options.GetClaimsFromUserInfoEndpoint = true;
        options.ClientId = "store";
        options.ClientSecret = "secret";
        options.ResponseType = "code";
        options.TokenValidationParameters.NameClaimType = "name";
        options.TokenValidationParameters.RoleClaimType = "role";
        options.Scope.Add("store");
        options.SaveTokens = true;
    });

// Configure options
builder.Services.Configure<ProductApiOptions>(builder.Configuration.GetSection(ProductApiOptions.SectionName));

// Configure services
builder.Services.AddHttpClient<IProductService, ProductService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IBaseService, BaseService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name : "default",
    pattern : "{controller=Home}/{action=Index}/{id?}");

app.Run();