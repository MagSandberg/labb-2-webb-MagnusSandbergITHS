using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebbLabb2.DataAccess.Sql.Models;

public class CustomerModel
{
    public Guid CustomerId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string CellNumber { get; set; } = string.Empty;
    public string StreetAddress { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public int ZipCode { get; set; }
}