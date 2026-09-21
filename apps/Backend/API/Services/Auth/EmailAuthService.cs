using Infrastructure.Model;
using Microsoft.AspNetCore.Identity;

namespace API.Services.Auth;

public class EmailAuthService(UserManager<Vgt7User> userManager)
{
    public async Task<(bool wasCreated, Vgt7User? user)> Register(string email, string password)
    {
        var newUser = new Vgt7User(email);
        var result = await userManager.CreateAsync(newUser, password);

        if (result.Succeeded)
            return (true, newUser);

        if (result.Errors.Any(e => e.Code is "DuplicateUserName" or "DuplicateEmail"))
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user != null) return (false, user);
        }

        var errors = string.Join("\n", result.Errors.Select(e => e.Description));
        throw new ArgumentException(errors);
    }

    public async Task<(bool isLogin, Vgt7User? user)> Login(string email, string password)
    {
        var user = await userManager.FindByEmailAsync(email);

        if (user == null)
            return (false, null);

        var isPasswordCorrect = await userManager.CheckPasswordAsync(user, password);

        if (isPasswordCorrect)
            return (true, user);

        return (false, null);
    }
}