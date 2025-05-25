using Domain.Common.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class ValueObjectConfigurationExtensions
{
    public static void ConfigureAddress<TOwner>(this OwnedNavigationBuilder<TOwner, Address> builder, string tablePrefix)
        where TOwner : class
    {
        builder.ToTable($"{tablePrefix}Addresses");
        builder.Property(a => a.Street).IsRequired().HasMaxLength(200);
        builder.Property(a => a.City).IsRequired().HasMaxLength(100);
        builder.Property(a => a.Country).IsRequired().HasMaxLength(100);
        builder.Property(a => a.Latitude);
        builder.Property(a => a.Longitude);
    }
}
