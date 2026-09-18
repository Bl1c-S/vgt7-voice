using Application.Models.Transcription;
using Application.Models.TranscriptionModel;
using Application.Services.Transcription.Helpers;
using Deepgram;
using Deepgram.Clients.Interfaces.v1;
using Deepgram.Models.Listen.v1.REST;

namespace Application.Services.Transcription.Deepgram;

public class DeepgramManager(TranscriptionModelDescriptor model, string apiKey) : TranscriptionManagerBase(model)
{
    private readonly DeepgramResponseMapper _deepgramResponseMapper = new();
    private readonly CallTranscriptBuilder _callTranscriptBuilder = new();
    
    private readonly IListenRESTClient _client = ClientFactory.CreateListenRESTClient(apiKey);
    
    public override async Task<CallTranscript> TranscribeAsync(byte[] audio, int managerChannel)
    {
        SyncResponse response;
        try
        {
            response = await _client.TranscribeFile(
                audio,
                new PreRecordedSchema
                {
                    Model = Model.Name,
                    Language = "ru",
                    MultiChannel = true,
                    Diarize = true,
                    Punctuate = true,
                    SmartFormat = true,
                    Utterances = true
                });
        }
        catch (Exception ex)
        {
            throw new HttpRequestException("Deepgram transcription request failed.", ex);
                //TODO Choose correct Ex + Logging
        }

        var channels = response.Results?.Channels;
        if (channels is null || channels.Count == 0)
            return new CallTranscript { Utterances = [], DurationSec = response.Metadata?.Duration ?? 0 };

        var words = _deepgramResponseMapper.MapToWords(channels);
        return _callTranscriptBuilder.Build(words, response.Metadata?.Duration ?? 0, managerChannel);
    }
}