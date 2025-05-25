using Domain.Accounts.Model;
using Domain.Accounts.ValueObjects;
using Infrastructure.Persistence.Configurations.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.AccountsCF;

internal class ReciptEntityConfiguration : IEntityTypeConfiguration<Receipt>
{
    public void Configure(EntityTypeBuilder<Receipt> builder)
    {
        builder.ToTable("Receipts");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Total).HasColumnType("decimal(18,2)");
        builder.Property(x => x.IsDeleted).HasDefaultValue(false);
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.UpdatedAt);
        builder.Property(x => x.DeletedAt);

        builder.Property(x => x.TransactionId).IsRequired();
        builder.HasOne(x => x.Transaction)
               .WithOne(t => t.Receipt)
               .HasForeignKey<Receipt>(x => x.TransactionId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.MerchantId).IsRequired();
        builder.HasOne(r => r.Merchant)
                .WithMany()
                .HasForeignKey(r => r.MerchantId)
                .OnDelete(DeleteBehavior.Cascade);

        builder.OwnsMany(x => x.Items, itemsBuilder =>
        {
            itemsBuilder.ToTable("ReceiptItems");
            itemsBuilder.WithOwner().HasForeignKey("ReceiptId");
            itemsBuilder.ConfigureReciptItems();
        });

        builder.OwnsOne(r => r.PaymentMethod, pmBuilder =>
        {
            pmBuilder.Property(pm => pm.Last4).HasMaxLength(4);
            pmBuilder.Property(pm => pm.Type)
                .HasConversion(
                    v => v.ToString(),
                    v => (PaymentMethod)Enum.Parse(typeof(PaymentMethod), v)
                );
        });
    }
}
