namespace API.Models.Requests;

public record LoginByEmailRequest(string Email, string Password);