using System.Security.Claims;
using IdentityModel;
using IdentityServerHost.Database;
using IdentityServerHost.Models;
using IdentityServerHost.Options;
using Microsoft.AspNetCore.Identity;

namespace IdentityServerHost.Initializer;

public class DbInitializer : IDbInitializer
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public DbInitializer(UserManager<ApplicationUser> userManager, 
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    /// <inheritdoc />
    public void Initialize()
    {
        var containedAdmin = _roleManager.FindByNameAsync(Roles.Admin).GetAwaiter().GetResult();
        if (containedAdmin is null)
        {
            _roleManager.CreateAsync(new IdentityRole(Roles.Admin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Roles.Customer)).GetAwaiter().GetResult();
        }
        else
        {
            return;
        }

        ApplicationUser admin = new()
        {
            UserName = "admin@gmail.com",
            Email = "admin@gmail.com",
            EmailConfirmed = true,
            PhoneNumber = "+7077777777",
            FirstName = "Kalibek",
            LastName = "Mergaziyev",
        };

        var crateUserResult = _userManager.CreateAsync(admin, "!Zt123456").GetAwaiter().GetResult();
        var addRoleResult = _userManager.AddToRoleAsync(admin, Roles.Admin).GetAwaiter().GetResult();

        _userManager.AddClaimsAsync(admin, new Claim[]
        {
            new Claim(JwtClaimTypes.Name, admin.FirstName + " " + admin.LastName),
            new Claim(JwtClaimTypes.GivenName, admin.FirstName),
            new Claim(JwtClaimTypes.FamilyName, admin.LastName),
            new Claim(JwtClaimTypes.Role, Roles.Admin),
        }).GetAwaiter().GetResult();
        
        ApplicationUser customer = new()
        {
            UserName = "customer@gmail.com",
            Email = "customer@gmail.com",
            EmailConfirmed = true,
            PhoneNumber = "+7078888888",
            FirstName = "Sanzhar",
            LastName = "Sabur",
        };

        _userManager.CreateAsync(customer, "!Zt123456").GetAwaiter().GetResult();
        _userManager.AddToRoleAsync(customer, Roles.Customer).GetAwaiter().GetResult();

        _userManager.AddClaimsAsync(customer, new Claim[]
        {
            new Claim(JwtClaimTypes.Name, customer.FirstName + " " + customer.LastName),
            new Claim(JwtClaimTypes.GivenName, customer.FirstName),
            new Claim(JwtClaimTypes.FamilyName, customer.LastName),
            new Claim(JwtClaimTypes.Role, Roles.Customer),
        }).GetAwaiter().GetResult();
    }
}