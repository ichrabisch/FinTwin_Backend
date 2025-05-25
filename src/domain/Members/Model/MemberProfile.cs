using Domain.Common.ValueObjects;
using Domain.Members.ValueObjects;
using SharedKernel.Abstractions;
using SharedKernel.Primitives;

namespace Domain.Members.Model;

public class MemberProfile : Entity, IAuditableEntity, IDeletableEntity
{
    protected MemberProfile() : base(Guid.NewGuid()) { } // This constructor is for EF Core

    public MemberProfile(
            Guid id,
            string name,
            string surname,
            EducationInformation educationLevel,
            EmploymentInformation employmentStatus,
            Address address,
            ResidentialInformation residentialInformation,
            FamilySize familySize,
            Member member
        ) : base(id)
    {
        Name = name;
        Surname = surname;
        EducationLevel = educationLevel;
        EmploymentStatus = employmentStatus;
        Address = address;
        ResidentialInformation = residentialInformation;
        FamilySize = familySize;
        Member = member;
        MemberId = member.Id;
        CreatedAt = DateTime.UtcNow;
    }

    public static MemberProfile Create(
               Guid id,
               string name,
               string surname,
               EducationInformation educationLevel,
               EmploymentInformation employmentStatus,
               Address address,
               ResidentialInformation residentialInformation,
               FamilySize familySize,
               Member member
           )
    {
        return new MemberProfile(id, name, surname, educationLevel, employmentStatus, address, residentialInformation, familySize, member);
    }

    public string Name { get; private set; }
    public string Surname { get; private set; }
    public EducationInformation EducationLevel { get; private set; }
    public EmploymentInformation EmploymentStatus { get; private set; }
    public Address Address { get; private set; }
    public ResidentialInformation ResidentialInformation { get; private set; }
    public FamilySize FamilySize { get; private set; }
    public int? EconomicLevelId { get; private set; }
    public Member Member { get; private set; }
    public Guid MemberId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTime? DeletedAt { get; private set; }

    public void SetLocation(float latitude, float longitude)
    {
        Address.Latitude = latitude;
        Address.Longitude = longitude;
    }
    public void SetEconomicLevel(int economicLevel)
    {
        EconomicLevelId = economicLevel;
    }
}