namespace Application.Models.Transcription;

public record TranscribedWord(int Channel, int? Speaker, string Text, decimal? Start, decimal? End);