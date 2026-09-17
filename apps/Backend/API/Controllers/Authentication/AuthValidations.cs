using API.Models.Requests;

namespace API.Controllers.Authentication;

public static class AuthValidations
{
    public static bool IsValidLogin(this LoginByEmailRequest byEmailRequest, out string message)
    {
        if (string.IsNullOrEmpty(byEmailRequest.Email) || string.IsNullOrEmpty(byEmailRequest.Password))
        {
            message = "Email or password are required.";
            return false;
        }

        message = string.Empty;
        return true;
    }
    public static bool IsValidLogin(this RegisterByEmailRequest byEmailRequest, out string message)
    {
        if (string.IsNullOrEmpty(byEmailRequest.Email) || string.IsNullOrEmpty(byEmailRequest.Password))
        {
            message = "Email or password are required.";
            return false;
        }

        message = string.Empty;
        return true;
    }
}