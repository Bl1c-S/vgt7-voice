using Application.Models.AiModel;

namespace Infrastructure.AI;

public class OpenAiManager : AiManagerBase
{
    public OpenAiManager(AiModelDescriptor model, string apiKey) : base(model)
    {
        
    }
    
    public override Task<string> SendRequestAsync(string prompt)
    {
        throw new NotImplementedException();
    }

    public override void Dispose()
    {
        throw new NotImplementedException();
    }
}