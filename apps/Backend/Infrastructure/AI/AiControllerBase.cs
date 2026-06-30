using Application;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.AI;

public class AiControllerBase : IAiController
{
    protected string ApiKey;
    protected readonly IConfiguration Configuration;
    protected virtual string ApiKeyConfigName => "AI_API_KEY";
    
    public AiControllerBase(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public virtual void Create()
    {
        ApiKey = Configuration[ApiKeyConfigName]
                 ?? throw new InvalidOperationException($"{ApiKeyConfigName} Not Found");
    }

    public virtual string GetResponse(string prompt)
    {
        throw  new NotImplementedException();
    }
}