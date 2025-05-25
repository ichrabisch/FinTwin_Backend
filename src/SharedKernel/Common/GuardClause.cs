using System.Text.RegularExpressions;
using FluentResults;

namespace SharedKernel.Common;
public static class GuardClause
{
    public static class Ensure 
    {
        public static Result<string> IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return Result.Fail<string>("Email cannot be null or empty.");
            }
            try
            {
                var regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", RegexOptions.IgnoreCase);
                if (regex.IsMatch(email))
                {
                    return Result.Ok(email);
                }
                else
                {
                    return Result.Fail<string>("Invalid email format.");
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return Result.Fail<string>("Email validation timed out.");
            }
        }
    }
}
