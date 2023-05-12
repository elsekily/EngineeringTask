
using Microsoft.EntityFrameworkCore;
using UserAPI.Entities.Models;

namespace UserAPI.Persistence;
public class UserDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public UserDbContext(DbContextOptions<UserDbContext> options)
    : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<User>()
            .HasIndex(u => u.UserName)
            .IsUnique();

        builder.Entity<User>()
            .HasKey(u => u.Id);

        builder.Entity<User>()
            .Property(u => u.UserName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Entity<User>()
            .Property(u => u.FirstName)
            .HasMaxLength(50);

        builder.Entity<User>()
            .Property(u => u.FatherName)
            .HasMaxLength(50);

        builder.Entity<User>()
            .Property(u => u.FamilyName)
            .HasMaxLength(50);

        builder.Entity<User>()
            .Property(u => u.HashedPassword)
            .IsRequired()
            .HasMaxLength(128);

        builder.Entity<User>()
            .Property(u => u.EncryptedData)
            .HasMaxLength(500);
    }
}