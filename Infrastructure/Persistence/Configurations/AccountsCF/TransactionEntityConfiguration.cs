using Domain.Accounts.Model;
using Domain.Accounts.ValueObjects;
using Domain.Members.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.AccountsCF;

public class TransactionEntityConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Transactions");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Amount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.TransactionType)
            .IsRequired()
            .HasConversion(
                v => v.ToString(),
                v => (TransactionType)Enum.Parse(typeof(TransactionType), v)
            );

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);

        builder.HasOne(x => x.Account)
            .WithMany(a => a.Transactions)  // Assuming Account has a Transactions collection
            .HasForeignKey(x => x.AccountId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Receipt)
            .WithOne(r => r.Transaction)
            .HasForeignKey<Receipt>(r => r.TransactionId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}