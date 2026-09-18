using Application.Models.TranscriptionModel;

namespace API.Models.Requests;

public record TranscribeCallRequest
{
    public required IFormFile Audio { get; init; }
    public int ManagerChannel { get; init; } = 0;
    public TranscriptionModelTypes Model { get; init; } = TranscriptionModelTypes.DeepgramNova3;
}