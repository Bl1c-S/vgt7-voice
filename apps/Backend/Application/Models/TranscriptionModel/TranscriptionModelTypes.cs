using System.ComponentModel;
using Application.Models.TranscriptionProvider;

namespace Application.Models.TranscriptionModel;

public enum TranscriptionModelTypes
{
    [TranscriptionModel(TranscriptionProvider.TranscriptionProviderTypes.Deepgram)]
    [Description("nova-3")]
    DeepgramNova3
}