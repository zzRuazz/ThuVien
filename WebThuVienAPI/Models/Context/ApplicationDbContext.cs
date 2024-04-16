using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace WebThuVienAPI.Models.Context;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Account> Account { get; set; }

    public DbSet<Category> Category { get; set; }

    public DbSet<Manufacture> Manufacture { get; set; }

    public DbSet<Product> Product { get; set; }

    public DbSet<ProductDetail> ProductDetail { get; set; }

    public DbSet<ProductImage> ProductImage { get; set; }

    public DbSet<ProductProperty> ProductProperty { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingInternal(modelBuilder);
    }

    internal static void OnModelCreatingInternal(ModelBuilder modelBuilder)
    {

    }
}
