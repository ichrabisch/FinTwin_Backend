
namespace Domain.Members.ValueObjects
{
    public enum EducationLevelCode
    {
        None=1,
        Primary,
        Secondary,
        Tertiary,
        PostGraduate,
        Doctorate
    }

    public sealed record EducationInformation
    {
        public string EducationLevelName { get; private set; }
        public EducationLevelCode EducationCode { get; private set; }

        public EducationInformation(EducationLevelCode educationCode)
        {
            EducationCode = educationCode;
            EducationLevelName = GetEducationLevel(educationCode);
        }

        private static string GetEducationLevel(EducationLevelCode educationCode)
        {
            return educationCode switch
            {
                EducationLevelCode.None => "None",
                EducationLevelCode.Primary => "Primary",
                EducationLevelCode.Secondary => "Secondary",
                EducationLevelCode.Tertiary => "Tertiary",
                EducationLevelCode.PostGraduate => "Post Graduate",
                EducationLevelCode.Doctorate => "Doctorate",
                _ => "None"
            };
        }
    }
}