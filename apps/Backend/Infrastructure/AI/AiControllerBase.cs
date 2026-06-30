using Application;

namespace Infrastructure.AI;

public abstract class AiControllerBase : IAiController
{
    protected object? Client;

    public virtual void Create()
    {
        //implementing an asp api connection
    }

    public abstract string GetResponse(string prompt);
}