using Application;
using Application.Models.AiModel;

namespace Infrastructure.AI;

public abstract class AiManager : IAiController, IDisposable
{
    public abstract AiModelDescriptor Model { get; set; }
    public abstract Task<string> SendRequestAsync(string prompt);

    public abstract void Dispose();
}