using SharedKernel.Primitives;

namespace Domain.Members.ValueObjects
{
    public enum EmploymentIndustry
    {
        Agriculture = 1,
        Mining,
        Construction,
        Manufacturing,
        Wholesale,
        Retail,
        Transportation,
        Information,
        Finance,
        RealEstate,
        Professional,
        Management,
        Administrative,
        WasteManagement,
        Educational,
        HealthCare,
        Arts,
        Entertainment,
        Recreation,
        Accommodation,
        FoodServices,
        OtherServices,
        PublicAdministration
    }

    public enum EmploymentStatus
    {
        Employed = 1,
        Unemployed,
        Student,
        Retired,
        Other
    }

    public sealed record EmploymentInformation
    {
        protected EmploymentInformation() { } // This constructor is for EF Core

        public EmploymentInformation(EmploymentIndustry industry, EmploymentStatus status)
        {
            EmploymentIndustry = industry;
            Status = status;
        }

        public EmploymentIndustry EmploymentIndustry { get; private set; }
        public EmploymentStatus Status { get; private set; }
    }
}