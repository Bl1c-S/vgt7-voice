using Application.Models.AiModel;
using Application.Models.AiProvider;
using Infrastructure.AI.Options;
using Microsoft.Extensions.Options;

namespace Infrastructure.AI;

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

    private static Func<AiModelDescriptor, string, AiManagerBase> SelectCreateMethod(AiProviderTypes modelType)
    {
        return modelType switch
        {
            AiProviderTypes.Gemini => CreateGm,
            AiProviderTypes.OpenAi => CreateOm,
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

    private string GetApiKey(AiProviderTypes provider)
    {
        var key = provider switch
        {
            AiProviderTypes.Gemini => _options.GEMINI_API_KEY,
            AiProviderTypes.OpenAi => _options.OPENAI_API_KEY,
            _ => throw new NotImplementedException()
        };

        if (string.IsNullOrWhiteSpace(key))
        {
            throw new InvalidOperationException($"API_KEY для провайдера {provider} не сконфигурирован.");
        }

        return key;
    }
    
}