using Application.Models.AiModel;

namespace Infrastructure.AI;

public class OpenAiManager : AiManagerBase
{
    public OpenAiManager(AiModelDescriptor model, string apiKey) : base(model)
    {
        throw new NotImplementedException();
    }
}