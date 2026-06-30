using Application.Models.AiModel;
using Google.GenAI;

namespace Infrastructure.AI;

public class GoogleAiManager(AiModelDescriptor model, string apiKey) : AiManagerBase(model)
{
    private readonly Client _client = new(apiKey: apiKey);

    public override async Task<string> SendRequestAsync(string prompt)
    {
        var response = await _client.Models.GenerateContentAsync(
            model: Model.Name,
            contents: prompt
        );
        
        return response.Text ?? throw new InvalidOperationException("EmptyResponse");
    }

    public override void Dispose()
    {
        _client.Dispose();
    }
}