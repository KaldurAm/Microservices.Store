#region

using System.ComponentModel.DataAnnotations;

#endregion

namespace Store.ProductApi.Models;

public class Product
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Range(1, 1000)]
    public decimal Price { get; set; }

    [StringLength(500)]
    public string Description { get; set; }

    [StringLength(200)]
    public string CategoryName { get; set; }

    [StringLength(500)]
    public string ImageUrl { get; set; }
}