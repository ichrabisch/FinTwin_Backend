using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Members.Model;
using Domain.Members.ValueObjects;
using Infrastructure.Persistence.Configurations.ValueObjects;

namespace Infrastructure.Persistence.Configurations;

public class MemberProfileConfiguration : IEntityTypeConfiguration<MemberProfile>
{
    public void Configure(EntityTypeBuilder<MemberProfile> builder)
    {
        builder.ToTable("MemberProfiles");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Surname).IsRequired().HasMaxLength(100);
        builder.Property(x => x.EconomicLevelId);
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.UpdatedAt);
        builder.Property(x => x.IsDeleted).IsRequired();
        builder.Property(x => x.DeletedAt);
        builder.Property(x => x.MemberId).IsRequired();
        builder.HasOne(x => x.Member)
               .WithOne(m => m.MemberProfile)
               .HasForeignKey<MemberProfile>(x => x.MemberId);
        builder.OwnsOne(x => x.EducationLevel, educationBuilder =>
        {
            educationBuilder.ToTable("EducationLevels");
            educationBuilder.WithOwner().HasForeignKey("MemberProfileId");
            educationBuilder.ConfigureEducationInformation();
        });
        builder.OwnsOne(x => x.EmploymentStatus, employmentBuilder =>
        {
            employmentBuilder.ToTable("EmploymentStatuses");
            employmentBuilder.WithOwner().HasForeignKey("MemberProfileId");
            employmentBuilder.ConfigureEmploymentInformation();
        });
        builder.OwnsOne(x => x.Address, addressBuilder =>
        {
            addressBuilder.ConfigureAddress("MemberProfile");
            addressBuilder.WithOwner().HasForeignKey("MemberProfileId");
        });
        builder.OwnsOne(x => x.ResidentialInformation, residentialBuilder =>
        {
            residentialBuilder.ToTable("ResidentialInformation");
            residentialBuilder.WithOwner().HasForeignKey("MemberProfileId");
            residentialBuilder.ConfigureResidentialInformation();
        });
        builder.OwnsOne(x => x.FamilySize, familySizeBuilder =>
        {
            familySizeBuilder.ToTable("FamilySizes");
            familySizeBuilder.WithOwner().HasForeignKey("MemberProfileId");
            familySizeBuilder.ConfigureFamilySize();
        });
    }
}