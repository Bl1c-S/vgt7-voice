using Application.Models.AiModel;
using Application.Models.Transcription;
using Application.Services.Transcription;
using Application.Services.Transcription.Deepgram;
using Deepgram;
using Deepgram.Clients.Interfaces.v1;
using Deepgram.Models.Listen.v1.REST;
using Infrastructure.AI;
using Serilog;

namespace Application.Services.AI;

public class DeepGramAiManager(AiModelDescriptor model, string apiKey) : AiManagerBase(model)
{
    private readonly IListenRESTClient _client = ClientFactory.CreateListenRESTClient(apiKey);

    public async Task<ConversationTranscript> SendRequestAsync(byte[] audio, int managerChannel = 0)
    {
        try
        {
            var response = await TranscribeFileAsync(audio);
            var duration = response.Metadata?.Duration ?? 0;
            var channels = response.Results?.Channels;

            if (channels is null || channels.Count == 0)
                return new ConversationTranscript([], duration);

            var words = DeepGramResponseMapper.MapToWords(channels);
            return ConversationTranscriptBuilder.Build(words, duration, managerChannel);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "DeepGram transcription request failed");
            throw;
        }
    }

    private async Task<SyncResponse> TranscribeFileAsync(byte[] audio) =>
        await _client.TranscribeFile(
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