using Google.GenAI;

namespace Infrastructure.AI;

public class GeminiController : AiControllerBase
{
    private const string Model = "gemini-2.0-flash"; 

    public override void Create() //Override
    {
        base.Create();

        var apiKey = System.Environment.GetEnvironmentVariable("GOOGLE_API_KEY")
                     ?? throw new InvalidOperationException("GOOGLE_API_KEY Not Found");

        Client = new Client(apiKey: apiKey);
    }

    public override string GetResponse(string prompt)
    {
        if (Client is not Client geminiClient)
            throw new InvalidOperationException("EmptyClient");

        return SendRequestAsync(geminiClient, prompt).GetAwaiter().GetResult();
    }

    private async Task<string> SendRequestAsync(Client geminiClient, string prompt)
    {
        var response = await geminiClient.Models.GenerateContentAsync(
            model: Model,
            contents: prompt
        );
        return response.Text ?? throw new InvalidOperationException("EmptyResponse");;
    }
}