using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Members.Model;
using Domain.Members.ValueObjects;

namespace Infrastructure.Persistence.Configurations.MemberCF;

public class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.ToTable("Members");

        builder.HasKey(x => x.Id);

        builder.HasIndex(p => p.Email).IsUnique();

        builder.Property(p => p.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(p => p.AccountType)
            .IsRequired()
            .HasConversion(
                v => v.ToString(),
                v => (AccountType)Enum.Parse(typeof(AccountType), v)
            );

        // Configure Password property
        builder.Property(p => p.Password)
            .IsRequired()
            .HasMaxLength(255);

        // Configure auditable properties
        builder.Property(p => p.CreatedAt)
            .IsRequired();
        builder.Property(p => p.UpdatedAt);

        // Configure soft delete properties
        builder.Property(p => p.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);
        builder.Property(p => p.DeletedAt);

        // Configure the relationship with MemberProfile
        builder.HasOne(m => m.MemberProfile)
            .WithOne(mp => mp.Member)
            .HasForeignKey<MemberProfile>(mp => mp.MemberId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure the relationship with Account
        builder.HasMany(m => m.Accounts)
            .WithOne(a => a.Member)
            .HasForeignKey(a => a.MemberId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
