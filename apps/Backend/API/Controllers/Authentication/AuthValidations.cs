using API.Models.Requests;

namespace API.Controllers.Authentication;

public static class AuthValidations
{
    public static bool IsValidRequest(this LoginByEmailRequest byEmailRequest)
    {
        if (string.IsNullOrEmpty(byEmailRequest.Email) || string.IsNullOrEmpty(byEmailRequest.Password))
            return false;
        return true;
    }

    public static bool IsValidRequest(this RegisterByEmailRequest byEmailRequest)
    {
        if (string.IsNullOrEmpty(byEmailRequest.Email) || string.IsNullOrEmpty(byEmailRequest.Password))
            return false;
        return true;
    }
}