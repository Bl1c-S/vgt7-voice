using Application.Models.AiModel;
using Application.Models.AiProvider;
using Infrastructure.AI;
using Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace Application.Services.AI;

public class AiManagerFactory(IOptions<AiOptions> options)
{
    private readonly AiOptions _options = options.Value;
    public AiManagerBase Create(AiModelTypes modelType)
    {
        var model = new AiModelDescriptor(modelType);
        var apiKey = GetApiKey(model.Provider);

        var factory = SelectCreateMethod(model.Provider);
        return factory(model, apiKey);
    }

    private static Func<AiModelDescriptor, string, AiManagerBase> SelectCreateMethod(AiProviderTypes providerType)
    {
        return providerType switch
        {
            AiProviderTypes.Gemini => CreateGm,
            AiProviderTypes.OpenAi => CreateOm,
            AiProviderTypes.DeepGram => CreateDg,
            _ => throw new NotImplementedException()
        };
    }

    private static GoogleAiManager CreateGm(AiModelDescriptor model, string apiKey)
    {
        return new GoogleAiManager(model, apiKey);
    }

    private static OpenAiManager CreateOm(AiModelDescriptor model, string apiKey)
    {
        return new OpenAiManager(model, apiKey);
    }
    
    private static DeepGramAiManager CreateDg(AiModelDescriptor model, string apiKey)
    {
        return new DeepGramAiManager(model, apiKey);
    }

    private string GetApiKey(AiProviderTypes provider)
    {
        return provider switch
        {
            AiProviderTypes.Gemini => _options.GeminiApiKey,
            AiProviderTypes.OpenAi => _options.OpenaiApiKey,
            AiProviderTypes.DeepGram => _options.DeepgramApiKey,
            _ => throw new InvalidOperationException($"API_KEY: {provider} not found.")
        };
    }
}