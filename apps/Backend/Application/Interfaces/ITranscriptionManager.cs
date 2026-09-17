using Application.Models.Transcription;
using Application.Models.TranscriptionModel;

namespace Application; //TODO change namaspace

public interface ITranscriptionManager
{
    protected TranscriptionModelDescriptor Model { get; set; }
    
    Task<CallTranscript> TranscribeAsync(byte[] audio);
}