using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using API.Models.Auth;
using API.Options;
using Infrastructure.Model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace API.Services.Auth;

public class TokenService(IOptions<AuthOptions> options)
{
    private readonly AuthOptions _options = options.Value;
    public Vgt7TokensDto Create(Vgt7User user)
    {
        var accessToken = CreateAccessToken(user);
        var refreshToken = CreateRefreshToken(user);
        return new Vgt7TokensDto(accessToken, refreshToken);
    }

    private Vgt7Token CreateAccessToken(Vgt7User user) => Create(user, TimeSpan.FromHours(2));

    private Vgt7Token CreateRefreshToken(Vgt7User user) => Create(user, TimeSpan.FromDays(7), "refresh_token");

    private Vgt7Token Create(Vgt7User user, TimeSpan lifeTime, string name = "access_token")
    {
        var claims = new List<Claim> { new(ClaimTypes.Name, user.UserName) };
        
        var credentials = new SigningCredentials(_options.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256);

        var jwt = new JwtSecurityToken(
            audience: "VGT7",
            claims: claims,
            expires: DateTime.UtcNow.Add(lifeTime),
            signingCredentials: credentials);

        var token = new JwtSecurityTokenHandler().WriteToken(jwt);
        return new Vgt7Token(user.Id, token, name);
    }
}