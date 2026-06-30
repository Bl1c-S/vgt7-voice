using Application.Models.AiModel;

namespace Infrastructure.AI;

public class AiManagerBase : AiManager
{
    public sealed override AiModelDescriptor Model { get; set; }
    
    protected AiManagerBase(AiModelDescriptor model)
    {
        Model = model;
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