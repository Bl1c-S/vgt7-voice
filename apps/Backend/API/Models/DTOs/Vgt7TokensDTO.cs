using API.Services.Auth;

namespace API.Models.Auth;

public record Vgt7TokensDto(Vgt7Token AccessToken, Vgt7Token RefreshToken);