using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace API.Options;

public class AuthOptions
{
    public string Secret { get; set; } = string.Empty;
    
    public SymmetricSecurityKey GetSymmetricSecurityKey() => new(Encoding.UTF8.GetBytes(Secret));
}