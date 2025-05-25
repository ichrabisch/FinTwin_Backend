using Domain.Accounts.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.AccountsCF;

public class AccountEntityConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("Accounts");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.AccountName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Balance)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(x => x.IsPersonal)
            .IsRequired();

        builder.Property(x => x.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.DeletedAt);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);

        builder.HasMany(x => x.Transactions)
            .WithOne(t => t.Account)
            .HasForeignKey(t => t.AccountId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Member)
            .WithMany(m => m.Accounts)
            .HasForeignKey(x => x.MemberId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.AccountName);
        builder.HasIndex(x => x.IsPersonal);
        builder.HasIndex(x => x.CreatedAt);

        builder.HasQueryFilter(x => !x.IsDeleted);

     
    }
}