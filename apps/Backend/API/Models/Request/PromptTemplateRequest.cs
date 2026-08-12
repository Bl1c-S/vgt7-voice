namespace API.Models.Request;

public record PromptTemplateRequest(bool IsEnable, string Name, string Prompt);