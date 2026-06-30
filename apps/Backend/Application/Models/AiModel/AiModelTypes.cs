using System.ComponentModel;

namespace Application.Models.AiModel;

public enum AiModelTypes
{
    [AiModel(AiProvider.AiProviderTypes.Gemini)]
    [Description("gemini-2.0-flash")]
    Gemini20Flash,
    
    [AiModel(AiProvider.AiProviderTypes.Gemini)]
    [Description("gemini-2.5-pro")]
    Gemini25Pro,

    [AiModel(AiProvider.AiProviderTypes.OpenAi)]
    [Description("gpt-4o")]
    Gpt4O
}