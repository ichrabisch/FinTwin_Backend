using FluentResults;
using SharedKernel.Abstractions;

namespace Application.Members.Commands.CreateMember;

public sealed record CreateMemberCommand(
    string Name,
    string Surname,
    string Email,
    string Password,
    string Country,
    string City,
    string Street,
    int AccountType,
    int EducationLevelCode,
    int EmploymentStatusCode,
    int EmploymentIndustryCode,
    int ResidentialStatusCode,
    int ResidentialTypeCode,
    int FamilySize
    ) : ICommand<Result<Guid>>;