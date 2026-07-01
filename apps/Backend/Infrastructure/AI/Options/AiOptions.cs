namespace Infrastructure.AI.Options;

public class AiOptions
{
    public const string SectionName = "Ai";

    public string GEMINI_API_KEY { get; set; } = string.Empty;
    public string OPENAI_API_KEY { get; set; } = string.Empty;
}