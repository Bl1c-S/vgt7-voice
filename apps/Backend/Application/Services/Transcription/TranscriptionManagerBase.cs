using Application.Models.Transcription;
using Application.Models.TranscriptionModel;

namespace Application.Services.Transcription;

public abstract class TranscriptionManagerBase(TranscriptionModelDescriptor model) : ITranscriptionManager
{
    public TranscriptionModelDescriptor Model { get; set; } = model;

    public virtual Task<CallTranscript> TranscribeAsync(byte[] audio)
    {
        throw new NotImplementedException();
    }
    public virtual void Dispose()
    {
        throw new NotImplementedException();
    }
}

    
