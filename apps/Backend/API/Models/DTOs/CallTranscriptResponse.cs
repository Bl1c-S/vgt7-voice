namespace API.Models.DTOs;

public record CallTranscriptResponse(
    List<UtteranceDto> Utterances,
    double DurationSec,
    string FullText,
    string? Summary);