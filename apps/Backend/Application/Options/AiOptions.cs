using Application.Models.AiModel;

namespace Infrastructure.Options;

public class AiOptions
{
    public string GeminiApiKey { get; set; } = string.Empty;
    public readonly AiModelTypes DefaultGoogleAiModel = AiModelTypes.Gemini20Flash;
    
    public string OpenaiApiKey { get; set; } = string.Empty;
    public readonly AiModelTypes DefaultOpenAiModel = AiModelTypes.Gpt4O;
    
}