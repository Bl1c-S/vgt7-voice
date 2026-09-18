namespace API.Models.DTOs;

public record UtteranceDto(decimal Start, decimal End, string Role, string Text);