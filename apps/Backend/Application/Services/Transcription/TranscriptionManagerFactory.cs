using Application.Models.TranscriptionModel;
using Application.Models.TranscriptionProvider;
using Application.Options;
using Application.Services.Transcription.Deepgram;
using Microsoft.Extensions.Options;

namespace Application.Services.Transcription;

public class TranscriptionManagerFactory(IOptions<TranscriptionOptions> options)
{
    private readonly TranscriptionOptions _options = options.Value;
    public TranscriptionManagerBase Create(TranscriptionModelTypes modelType)
    {
        var model = new TranscriptionModelDescriptor(modelType);
        var apiKey = GetApiKey(model.Provider);

        var factory = SelectCreateMethod(model.Provider);
        return factory(model, apiKey);
    }
    
    private static Func<TranscriptionModelDescriptor, string, TranscriptionManagerBase> SelectCreateMethod(TranscriptionProviderTypes modelType)
    {
        return modelType switch
        {
            TranscriptionProviderTypes.Deepgram => CreateDm,
            _ => throw new NotImplementedException()
        };
    }
    
    private static DeepgramManager CreateDm(TranscriptionModelDescriptor model, string apiKey)
    {
        return new DeepgramManager(model, apiKey);
    }
    
    private string GetApiKey(TranscriptionProviderTypes provider)
    {
        return provider switch
        {
            TranscriptionProviderTypes.Deepgram => _options.DeepgramApiKey,
            _ => throw new InvalidOperationException($"API_KEY: {provider} not found.")
        };
    }
}
