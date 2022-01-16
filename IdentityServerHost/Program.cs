using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using IdentityServerHost.Database;
using IdentityServerHost.Initializer;
using IdentityServerHost.Models;
using IdentityServerHost.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Configure sql server database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

var identityServerBuilder = builder.Services.AddIdentityServer(options =>
    {
        options.Events.RaiseErrorEvents = true;
        options.Events.RaiseInformationEvents = true;
        options.Events.RaiseFailureEvents = true;
        options.Events.RaiseSuccessEvents = true;
        options.EmitStaticAudienceClaim = true;
    })
    .AddInMemoryIdentityResources(Roles.IdentityResources)
    .AddInMemoryApiScopes(Roles.ApiScopes)
    .AddInMemoryClients(Roles.Clients)
    .AddAspNetIdentity<ApplicationUser>();

identityServerBuilder.AddDeveloperSigningCredential();

// Configure services
builder.Services.AddScoped<IDbInitializer, DbInitializer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();
app.MapRazorPages();
var provider = builder.Services.BuildServiceProvider();
var dependency = provider.GetRequiredService<IDbInitializer>();
dependency.Initialize();
app.Run();