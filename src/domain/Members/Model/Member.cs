using Domain.Accounts.Model;
using Domain.Common.ValueObjects;
using Domain.Members.ValueObjects;
using SharedKernel.Abstractions;
using SharedKernel.Common;
using SharedKernel.Primitives;

namespace Domain.Members.Model;

public class Member : AggregateRoot, IDeletableEntity, IAuditableEntity
{
    private Member(Guid id, string email, string password, AccountType accountType) : base(id)
    {
        Email = email;
        Password = password;
        AccountType = accountType;
    } 
    public List<Account> Accounts { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public AccountType AccountType { get; private set; }
    public MemberProfile MemberProfile { get; private set; }

    public bool IsDeleted { get; private set;}

    public DateTime? DeletedAt { get; private set;}

    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; private set; }

    public static Member Create(Guid id, string email, string password, AccountType accountType)
    {
        ArgumentException.ThrowIfNullOrEmpty(email);
        ArgumentException.ThrowIfNullOrEmpty(password);

        var emailResult = GuardClause.Ensure.IsValidEmail(email);

        if (!emailResult.IsSuccess) throw new ArgumentException(emailResult.Reasons[0].ToString());

        return new Member(id, email, password, accountType);
    }

    public void AddProfile(
        string name,
        string surname,
        EducationInformation educationLevel,
        EmploymentInformation employmentStatus,
        Address address,
        ResidentialInformation residentialInformation,
        FamilySize familySize)
    {
        if (MemberProfile != null) throw new InvalidOperationException("Profile already exists");

        MemberProfile = MemberProfile.Create(
            Guid.NewGuid(),
            name,
            surname,
            educationLevel,
            employmentStatus,
            address,
            residentialInformation,
            familySize,
            this);
    }

    public void AddAccount(Account account)
    {
        Accounts.Add(account);
    }

}
