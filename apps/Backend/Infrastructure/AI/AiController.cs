using Application;

namespace Infrastructure.AI;

public abstract class AiController : IAiController
{
    public abstract void Create();
    public abstract string GetResponse(string prompt);
}