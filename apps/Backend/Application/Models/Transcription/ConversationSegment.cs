
namespace Application.Models.Transcription;

public class ConversationSegment
{
    public required decimal Start { get; init; }
    public required decimal End { get; init; }
    public required int Channel { get; init; }
    public int? Speaker { get; init; } 
    public required InterlocutorTypes Role { get; init; }
    public required string Text { get; init; }
}