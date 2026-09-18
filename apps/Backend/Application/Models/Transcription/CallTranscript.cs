namespace Application.Models.Transcription;

public class CallTranscript
{
    public required List<Utterance> Utterances { get; init; }
    public required double DurationSec { get; init; }
    public string FullText => BuildFullText();
    public string? Summary { get; set; } 

    private string BuildFullText()
    {
        return string.Join(
            Environment.NewLine,
            Utterances.Select(u => $"{u.Role}: {u.Text}"));
    }
}