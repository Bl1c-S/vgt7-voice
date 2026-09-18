
namespace Application.Models.Transcription;

public class Utterance
{
    public required decimal Start { get; init; }
    public required decimal End { get; init; }
    public required int Channel { get; init; }
    public int? Speaker { get; init; } 
    public required CallRole Role { get; init; }
    public required string Text { get; init; }
}