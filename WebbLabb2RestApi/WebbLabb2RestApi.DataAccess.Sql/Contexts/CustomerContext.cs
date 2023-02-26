using Microsoft.EntityFrameworkCore;
using WebbLabb2RestApi.DataAccess.Sql.Models;

namespace WebbLabb2RestApi.DataAccess.Sql.Contexts;

public class CustomerContext : DbContext
{
    public DbSet<CustomerModel> CustomerModel { get; set; } = null!;

    public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=CustomerDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

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