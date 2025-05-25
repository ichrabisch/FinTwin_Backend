using FluentValidation;

namespace Application.Members.Commands.CreateMember;

public class CreateMemberCommandValidator : AbstractValidator<CreateMemberCommand>
{
    public CreateMemberCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required").WithErrorCode("ERR_NAME_REQUIRED");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("A valid email is required").WithErrorCode("ERR_EMAIL_INVALID");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required").WithErrorCode("ERR_PASSWORD_REQUIRED");
        RuleFor(x => x.Country).NotEmpty().WithMessage("Country is required").WithErrorCode("ERR_COUNTRY_REQUIRED");
        RuleFor(x => x.City).NotEmpty().WithMessage("City is required").WithErrorCode("ERR_CITY_REQUIRED");
        RuleFor(x => x.Street).NotEmpty().WithMessage("Street is required").WithErrorCode("ERR_STREET_REQUIRED");
        RuleFor(x => x.AccountType).NotEmpty().WithMessage("Account type is invalid").WithErrorCode("ERR_ACCOUNT_TYPE_INVALID");
        RuleFor(x => x.EducationLevelCode).NotEmpty().WithMessage("Education level code is invalid").WithErrorCode("ERR_EDUCATION_LEVEL_INVALID");
        RuleFor(x => x.EmploymentStatusCode).NotEmpty().WithMessage("Employment status code is invalid").WithErrorCode("ERR_EMPLOYMENT_STATUS_INVALID");
        RuleFor(x => x.EmploymentIndustryCode).NotEmpty().WithMessage("Employment industry code is invalid").WithErrorCode("ERR_EMPLOYMENT_INDUSTRY_INVALID");
        RuleFor(x => x.ResidentialStatusCode).NotEmpty().WithMessage("Residential status code is invalid").WithErrorCode("ERR_RESIDENTIAL_STATUS_INVALID");
        RuleFor(x => x.ResidentialTypeCode).NotEmpty().WithMessage("Residential type code is invalid").WithErrorCode("ERR_RESIDENTIAL_TYPE_INVALID");
        RuleFor(x => x.FamilySize).GreaterThan(0).WithMessage("Family size must be greater than 0").WithErrorCode("ERR_FAMILY_SIZE_INVALID");
    }
}
