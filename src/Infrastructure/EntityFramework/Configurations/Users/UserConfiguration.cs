using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Users.Domain.Aggregates.UserAggregate.Entities;
using Users.Domain.ValueObjects;

namespace Users.Infrastructure.EntityFramework.Configurations.Users;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.OwnsOne(u => u.Name, e =>
        {
            e.Property(z => z.FirstName)
                .HasColumnName("first_name")
                .HasColumnType($"character varying({Name.MaxFirstNameLength})")
                .HasMaxLength(Name.MaxFirstNameLength)
                .IsRequired();

            e.Property(z => z.LastName)
                .HasColumnType($"character varying({Name.MaxLastNameLength})")
                .HasColumnName("last_name")
                .HasMaxLength(Name.MaxLastNameLength)
                .IsRequired();
        });

        builder.OwnsOne(u => u.Nickname, e =>
        {
            e.Property(z => z.Value)
                .HasColumnName("nickname")
                .HasColumnType($"character varying({Nickname.MaxNickNameLength})")
                .HasMaxLength(Nickname.MaxNickNameLength)
                .IsRequired();
        });

        builder.OwnsOne(u => u.Email, e =>
        {
            e.Property(z => z.Address)
                .HasColumnName("email")
                .HasColumnType($"character varying({Email.MaxEmailLength})")
                .HasMaxLength(Email.MaxEmailLength)
                .IsRequired();

            e.HasIndex(z => z.Address).IsUnique();
        });

        builder.OwnsOne(u => u.Phone, e =>
        {
            e.Property(z => z.IDD)
                .HasColumnName("idd")
                .HasColumnType($"character varying({Phone.MaxIddLength})")
                .HasMaxLength(Phone.MaxIddLength);

            e.Property(z => z.Number)
                .HasColumnName("phone")
                .HasColumnType($"character varying({Phone.MaxNumberLength})")
                .HasMaxLength(Phone.MaxNumberLength);
        });

        builder.Property(e => e.IsActive)
            .HasColumnName("active");

        builder.ConfigureAuditableEntity();
    }
}