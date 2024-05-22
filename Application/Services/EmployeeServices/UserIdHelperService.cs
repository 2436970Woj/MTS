namespace Application.Services.EmployeeServices;

public class UserIdHelperService
{
    public static string GetUserIdWithoutDomain(string userId)
    {
        if (!string.IsNullOrEmpty(userId))
        {
            int index = userId.IndexOf("\\");
            if (index != -1 && index < userId.Length - 1)
            {
                return userId[(index + 1)..];
            }
        }

        return userId;
    }
}
