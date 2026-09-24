namespace Application.Models.Transcription;

public class ConversationTranscript(List<ConversationSegment> conversation, double durationSec)
{
    public List<ConversationSegment> Conversation { get; init; } = conversation;
    public double DurationSec { get; init; } = durationSec;
    public string FullText => BuildFullText();
    public string? Summary { get; set; }

    private string BuildFullText()
    {
        return string.Join(
            Environment.NewLine,
            Conversation.Select(u => $"{u.Role}: {u.Text}"));
    }
}