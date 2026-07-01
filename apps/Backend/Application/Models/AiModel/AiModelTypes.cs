using System.ComponentModel;

namespace Application.Models.AiModel;

public enum AiModelTypes
{
    [AiModel(AiProvider.AiProviderTypes.Gemini)]
    [Description("gemini-2.5-flash")] //Gemini20Flash - not working 
    Gemini20Flash,                         //gemini-2.5-flash - 5 RPM; 20 RPD
                                           //gemini gemini-2.5-flash-lite 10 RPM; 20 RPD Should make retry policy cuz low free tier have low priority
                                           
    [AiModel(AiProvider.AiProviderTypes.Gemini)]
    [Description("gemini-2.5-pro")]
    Gemini25Pro,

    [AiModel(AiProvider.AiProviderTypes.OpenAi)]
    [Description("gpt-4o")]
    Gpt4O
}