using Infrastructure.Model;
using Microsoft.AspNetCore.Identity;

namespace API.Services.Auth;

public class AuthService(UserManager<Vgt7User> userManager)
{
    public bool Register(string email, string password, out Vgt7User? user)
    {
        user = userManager.FindByEmailAsync(email).Result;
        if (user != null) return false;
        
        user = new Vgt7User(email);
        userManager.CreateAsync(user, password);
        return true;
    }

    public bool Login(string email, string password, out Vgt7User? user)
    {
        user = userManager.FindByEmailAsync(email).Result;
        return user != null && userManager.CheckPasswordAsync(user, password).Result;
    }
}