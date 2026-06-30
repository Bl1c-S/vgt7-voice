using System.ComponentModel;

namespace Application.Models.AiProvider;

public enum AiProviderTypes
{
    [Description("GEMINI_API_KEY")]
    Gemini,
    [Description("empty")] //TODO
    OpenAi
}