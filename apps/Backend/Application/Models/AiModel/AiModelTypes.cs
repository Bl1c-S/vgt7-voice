using System.ComponentModel;

namespace Application.Models.AiModel;

public enum AiModelTypes
{
    [AiModel(AiProvider.AiProviderTypes.Gemini)]
    [Description("gemini-3.1-flash-lite")]
    Gemini20Flash,
    
    [AiModel(AiProvider.AiProviderTypes.Gemini)]
    [Description("gemini-2.5-pro")]
    Gemini25Pro,

    [AiModel(AiProvider.AiProviderTypes.OpenAi)]
    [Description("gpt-4o")]
    Gpt4O
}