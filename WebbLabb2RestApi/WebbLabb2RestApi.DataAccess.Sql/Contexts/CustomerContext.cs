using Microsoft.EntityFrameworkCore;
using WebbLabb2RestApi.DataAccess.Sql.Models;

namespace WebbLabb2RestApi.DataAccess.Sql.Contexts;

public class CustomerContext : DbContext
{
    public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
    {
    }

    public DbSet<CustomerModel> CustomerModel { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomerModel>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK_Customer_Model");

            entity.Property(e => e.CustomerId).ValueGeneratedOnAdd();
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(255).IsRequired();
            entity.Property(e => e.CellNumber).HasMaxLength(20);
            entity.Property(e => e.StreetAddress).HasMaxLength(100);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.ZipCode).HasMaxLength(5);

        });
    }
}