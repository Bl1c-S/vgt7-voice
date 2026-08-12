using API.Models.Request;

namespace API.Controllers.Authentication;

public static class AuthValidations
{
    public static bool IsValidLogin(this LoginRequest request, out string message)
    {
        if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
        {
            message = "Email or password are required.";
            return false;
        }

        message = string.Empty;
        return true;
    }
}