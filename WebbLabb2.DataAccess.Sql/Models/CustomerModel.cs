using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebbLabb2.DataAccess.Sql.Models;

public class CustomerModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid CustomerId { get; set; }
    [MaxLength(50), Required]
    public string FirstName { get; set; } = string.Empty;
    [MaxLength(50), Required]
    public string LastName { get; set; } = string.Empty;
    [EmailAddress, Required]
    public string Email { get; set; } = string.Empty;
    //TODO Kolla vilken datatyp Phone vill använda, just nu köper den allt
    [Phone, Required]
    public string CellNumber { get; set; } = string.Empty;
    [MaxLength(100), Required]
    public string StreetAddress { get; set; } = string.Empty;
    [MaxLength(50), Required]
    public string City { get; set; } = string.Empty;
    [MaxLength(6), Required]
    public int ZipCode { get; set; }
}