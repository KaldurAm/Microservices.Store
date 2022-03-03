#region

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.IdentityServer.Database;
using Store.IdentityServer.Initializer;
using Store.IdentityServer.Models;
using Store.IdentityServer.Options;

#endregion

var builder = WebApplication.CreateBuilder(args);

// Add web dependencies
builder.Services.AddRazorPages();

// Add services to the DI container.
//builder.Services.AddScoped<IDbInitializer, DbInitializer>();

// Add sql server database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddIdentityServer(options =>
    {
        options.Events.RaiseErrorEvents = true;
        options.Events.RaiseInformationEvents = true;
        options.Events.RaiseFailureEvents = true;
        options.Events.RaiseSuccessEvents = true;
        options.EmitStaticAudienceClaim = true;
    }).AddInMemoryIdentityResources(Roles.IdentityResources).AddInMemoryApiScopes(Roles.ApiScopes)
    .AddInMemoryClients(Roles.Clients).AddAspNetIdentity<ApplicationUser>().AddDeveloperSigningCredential();

//builder.Services.BuildServiceProvider().GetRequiredService<IDbInitializer>().Initialize();

// Build Application and configure application pipeline
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
app.MapRazorPages().RequireAuthorization();
app.Run();