using Application.Models.TranscriptionModel;

namespace Application.Options;

public class TranscriptionOptions
{
    public string DeepgramApiKey { get; set; } = string.Empty;
    public readonly TranscriptionModelTypes DefaultDeepgramModel = TranscriptionModelTypes.DeepgramNova3;
}