using Domain.Accounts.Model;
using Domain.Accounts.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.ValueObjects;

public static partial class ValueObjectConfigurationExtensions
{
    public static void ConfigureReciptItems(this OwnedNavigationBuilder<Receipt, ReceiptItem> builder)
    {
        builder.Property(item => item.ItemName).IsRequired().HasMaxLength(100);
        builder.Property(item => item.Category).IsRequired().HasMaxLength(50);
        builder.Property(item => item.GeneralCategory).IsRequired().HasMaxLength(50);
        builder.Property(item => item.ItemDescription).HasMaxLength(200);
        builder.Property(item => item.Unit).IsRequired().HasMaxLength(50);

        builder.Property(item => item.TaxRate).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(item => item.Quantity).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(item => item.UnitPrice).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(item => item.TotalPrice).IsRequired().HasColumnType("decimal(18,2)");
        
    }

}
