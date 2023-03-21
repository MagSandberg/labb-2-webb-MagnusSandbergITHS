using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace WebbLabb2.Shared.DTOs;

public class CustomerDto
{
    public Guid Id { get; set; }
    [Required]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Name is too long or short.")]
    public string FirstName { get; set; } = string.Empty;
    [Required]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Name is too long or short.")]
    public string LastName { get; set; } = string.Empty;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string CellNumber { get; set; } = string.Empty;
    [Required]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Name is too long or short.")]
    public string StreetAddress { get; set; } = string.Empty;
    [Required]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Name is too long or short.")]
    public string City { get; set; } = string.Empty;
    [Required]
    public int ZipCode { get; set; }
}