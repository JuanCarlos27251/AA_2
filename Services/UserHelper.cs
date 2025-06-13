using System.Security.Claims;

namespace AA2.Services
{

    public static class UserHelper
{
    public static int GetCurrentUserId(ClaimsPrincipal user)
    {
        var userIdString = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return int.Parse(userIdString);
    }

    public static bool IsAdmin(ClaimsPrincipal user)
    {
        return user.IsInRole("Admin");
    }
}

}

