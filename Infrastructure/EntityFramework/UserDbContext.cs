using Domain.Entities;
using Domain.ValueObjects.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework;

public class UserDbContext(DbContextOptions<UserDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasKey(u => u.Id);

        modelBuilder.Entity<User>()
            .Property(u => u.Email)
            .HasColumnName("Email")
            .HasConversion(email => email.Value, value => new Email(value));

        modelBuilder.Entity<User>()
            .Property(u => u.Username)
            .HasColumnName("Username")
            .HasConversion(username => username.Value, value => new Username(value));

        modelBuilder.Entity<User>()
            .Property(u => u.PasswordHash)
            .HasColumnName("PasswordHash")
            .HasConversion(passwordHash => passwordHash.Value, value => new PasswordHash(value));
    }
}