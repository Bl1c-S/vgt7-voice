using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Model;

public sealed class Vgt7User : IdentityUser
{
    private static readonly Regex EmailRegex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

    [AllowNull]
    public override string UserName
    {
        get => base.UserName ?? string.Empty;
        set => base.UserName = value;
    }

    [AllowNull]
    public override string Email
    {
        get => base.Email ?? string.Empty;
        set => base.Email = value;
    }

    /// <summary>
    /// Конструктор без параметров для EF Core и UserManager
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public Vgt7User() : base()
    {
    }

    public Vgt7User(string email) : this()
    {
        ValidateEmailFormat(email);

        Email = email;
        NormalizedEmail = Email.ToLowerInvariant();

        UserName = ExtractUsernameFromEmail(Email);
    }

    private static string ExtractUsernameFromEmail(string email)
    {
        var atIndex = email.IndexOf('@');
        return atIndex > 0 ? email.Substring(0, atIndex) : email;
    }

    private static void ValidateEmailFormat(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Email cannot be empty.", nameof(email));
        }

        if (!EmailRegex.IsMatch(email))
        {
            throw new ArgumentException("Invalid Email format.", nameof(email));
        }
    }
}