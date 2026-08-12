using Microsoft.AspNetCore.Identity;

namespace API.Services.Auth;

public sealed class Vgt7Token : IdentityUserToken<string>
{
    public Vgt7Token(string userid, string token, string name)
    {
        UserId = userid;
        LoginProvider = "JWT";
        Name = name;
        Value = token;
    }
}