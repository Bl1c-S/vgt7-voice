using Application.Models.AiModel;
using Application.Models.AiProvider;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.AI;

public class AiManagerFactory(IConfiguration configuration)
{
    public AiManagerBase Create(AiModelTypes modelType)
    {
        var model = new AiModelDescriptor(modelType);
        var apiKey = GetApiKey(configuration, model.ApiKeyConfigName);

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

    private static string GetApiKey(IConfiguration configuration, string apiKeyConfigName)
    {
        return configuration[apiKeyConfigName]
               ?? throw new InvalidOperationException($"API_KEY: {apiKeyConfigName} not found");
    }
}