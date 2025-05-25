using Application.Members.Commands.CreateMember;

namespace WebAPI.V1.Members.Endpoints.Handlers
{
    public sealed record CreateMemberHandlerRequest(
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
        )
    {
        public CreateMemberCommand ToCommand() => new CreateMemberCommand(
                Name,
                Surname,
                Email,
                Password,
                Country,
                City,
                Street,
                AccountType,
                EducationLevelCode,
                EmploymentStatusCode,
                EmploymentIndustryCode,
                ResidentialStatusCode,
                ResidentialTypeCode,
                FamilySize
            );
    }
}
