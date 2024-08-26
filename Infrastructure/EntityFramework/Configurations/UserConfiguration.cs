using Domain.Entities;
using Domain.ValueObjects.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFramework.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Email)
                .HasConversion(email => email.Value, value => new Email(value));

            builder.Property(u => u.Username)
                .HasConversion(username => username.Value, value => new Username(value));

            builder.Property(u => u.PasswordHash)
                .HasConversion(passwordHash => passwordHash.Value, value => new PasswordHash(value));

            builder.Ignore(u => u.IsSignIn);
        }
    }
}
