using Application;
using Application.Models.AiModel;

namespace Infrastructure.AI;

public abstract class AiManagerBase(AiModelDescriptor model) : IAiManager, IDisposable
{
    public AiModelDescriptor Model { get; set; } = model;

    public virtual Task<string> SendRequestAsync(string prompt)
    {
        throw new NotImplementedException();
    }

    public virtual void Dispose()
    {
        throw new NotImplementedException();
    }
}