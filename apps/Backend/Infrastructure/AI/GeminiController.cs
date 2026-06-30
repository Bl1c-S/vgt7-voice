using Google.GenAI;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.AI;

public class GeminiController : AiControllerBase
{
    private Client? _geminiClient;
    private const string Model = "gemini-2.0-flash";
    protected override string ApiKeyConfigName => "GEMINI_API_KEY";
    
    public GeminiController(IConfiguration configuration) : base(configuration)
    {
    }

    public override void Create() 
    {
        base.Create();
        _geminiClient = new Client(apiKey: ApiKey); 
    }

    public override string GetResponse(string prompt)
    {
        if (_geminiClient == null)
            throw new InvalidOperationException("Gemini client is not initialized");

        if (string.IsNullOrWhiteSpace(prompt))
            throw new ArgumentException("prompt is empty", nameof(prompt));
        
        return SendRequestAsync(_geminiClient, prompt).GetAwaiter().GetResult(); //TODO Await 
    }

    private async Task<string> SendRequestAsync(Client geminiClient, string prompt)
    {
        var response = await geminiClient.Models.GenerateContentAsync(
            model: Model,
            contents: prompt
        );
        return response.Text ?? throw new InvalidOperationException("EmptyResponse");
    }
}