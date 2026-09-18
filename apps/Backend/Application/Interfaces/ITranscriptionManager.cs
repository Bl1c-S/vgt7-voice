using Application.Models.Transcription;
using Application.Models.TranscriptionModel;

namespace Application; //TODO change namespace

public interface ITranscriptionManager
{ 
    TranscriptionModelDescriptor Model { get; set; }
    
    Task<CallTranscript> TranscribeAsync(byte[] audio, int managerChannel);
}