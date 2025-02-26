﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

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
    [Phone, Required]
    public string CellNumber { get; set; } = string.Empty;
    [MaxLength(100), Required]
    public string StreetAddress { get; set; } = string.Empty;
    [MaxLength(50), Required]
    public string City { get; set; } = string.Empty;
    [MaxLength(6), Required]
    public int ZipCode { get; set; }
}