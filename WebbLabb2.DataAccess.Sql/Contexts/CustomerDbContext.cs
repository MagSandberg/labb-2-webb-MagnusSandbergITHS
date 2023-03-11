using Microsoft.EntityFrameworkCore;
using WebbLabb2.DataAccess.Sql.Models;

namespace WebbLabb2.DataAccess.Sql.Contexts;

public class CustomerDbContext : DbContext
{
    public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
    {
    }

    public DbSet<CustomerModel> CustomerModel { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomerModel>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK_Customer_Model");
            entity.HasAlternateKey(e => e.Email);
        });
    }
}