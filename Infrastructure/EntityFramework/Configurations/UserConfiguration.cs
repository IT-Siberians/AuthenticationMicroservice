using Common.Helpers.Constants;
using Domain.Entities;
using Domain.Helpers.PasswordHashHelpers;
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
                .HasMaxLength(EmailConstants.EMAIL_MAX_LENGTH)
                .HasConversion(email => email.Value, value => new Email(value));

            builder.Property(u => u.Username)
                .HasMaxLength(UsernameConstants.USERNAME_MAX_LENGTH)
                .HasConversion(username => username.Value, value => new Username(value));

            builder.Property(u => u.PasswordHash)
                .HasMaxLength(PasswordHashConstants.PASSWORDHASH_MAX_LENGTH)
                .HasConversion(passwordHash => passwordHash.Value, value => new PasswordHash(value));

            builder.Ignore(u => u.IsSignIn);
        }
    }
}
