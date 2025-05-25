using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Members.ValueObjects;
using Domain.Members.Model;
using Domain.Common.ValueObjects;

namespace Infrastructure.Persistence.Configurations.ValueObjects;

public static partial class ValueObjectConfigurationExtensions
{

    public static void ConfigureEducationInformation(this OwnedNavigationBuilder<MemberProfile, EducationInformation> builder)
    {
        builder.Property(e => e.EducationLevelName).IsRequired().HasMaxLength(50);
        builder.Property(e => e.EducationCode)
            .HasConversion<string>()
            .IsRequired()
            .HasMaxLength(20);
    }

    public static void ConfigureEmploymentInformation(this OwnedNavigationBuilder<MemberProfile, EmploymentInformation> builder)
    {
        builder.Property(e => e.EmploymentIndustry)
            .HasConversion<string>()
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(e => e.Status)
            .HasConversion<string>()
            .IsRequired()
            .HasMaxLength(20);
    }

    public static void ConfigureFamilySize(this OwnedNavigationBuilder<MemberProfile, FamilySize> builder)
    {
        builder.Property(f => f.Size).IsRequired();
    }

    public static void ConfigureResidentialInformation(this OwnedNavigationBuilder<MemberProfile, ResidentialInformation> builder)
    {
        builder.Property(r => r.Status)
            .HasConversion<string>()
            .IsRequired()
            .HasMaxLength(20);
        builder.Property(r => r.Type)
            .HasConversion<string>()
            .IsRequired()
            .HasMaxLength(20);
    }
}