using System.ComponentModel.DataAnnotations;

namespace WebbLabb2.Shared.DTOs;

public class ProductDto
{
    public string ProductId { get; set; } = string.Empty;
    [Required]
    public int ProductNumber { get; set; }
    [Required]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Name is too long or short.")]
    public string ProductName { get; set; } = string.Empty;
    public string ProductDescription { get; set; } = string.Empty;
    [Required]
    public float ProductPrice { get; set; }
    public string ProductCategory { get; set; } = string.Empty;
    public int OrderCount { get; set; }
    public bool ProductStatus { get; set; }
    public string ProductImage { get; set; } = string.Empty;
}