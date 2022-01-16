using Microsoft.EntityFrameworkCore;
using Store.ProductApi.Models;

namespace Store.ProductApi.Database;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        
    }
    
    public DbSet<Product>? Products { get; set; }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 1,
            Name = "Samosa",
            Price = 15m,
            Description =
                "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.",
            ImageUrl = "https://microservicecourse.blob.core.windows.net/store/14.jpg",
            CategoryName = "Appetizer"
        });
        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 2,
            Name = "Paneer Tikka",
            Price = 13.99m,
            Description =
                "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.",
            ImageUrl = "https://microservicecourse.blob.core.windows.net/store/12.jpg",
            CategoryName = "Appetizer"
        });
        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 3,
            Name = "Sweet Pie",
            Price = 10.99m,
            Description =
                "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.",
            ImageUrl = "https://microservicecourse.blob.core.windows.net/store/11.jpg",
            CategoryName = "Dessert"
        });
        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 4,
            Name = "Pav Bhaji",
            Price = 15m,
            Description =
                "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.",
            ImageUrl = "https://microservicecourse.blob.core.windows.net/store/13.jpg",
            CategoryName = "Entree"
        });
    }
}