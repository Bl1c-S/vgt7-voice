using Application.Models.AiProvider;
using Application.Models.Extensions;

namespace Application.Models.AiModel;

public class AiModelDescriptor
{
    public AiModelTypes Type
    {
        get;
        set
        {
            if (value.GetAiProvider() == Provider)
                field = value;
            else
                throw new InvalidOperationException("InvalidModel");
        }
    }

    public readonly string Name;
    
    public readonly AiProviderTypes Provider;
    
    public readonly string ApiKeyConfigName;
    
    public AiModelDescriptor(AiModelTypes type)
    {
        Provider = type.GetAiProvider(); //must be first! check factory tests
        Name = type.GetDescription();
        ApiKeyConfigName = Provider.GetDescription(); //TODO remove
        
        Type = type;
    }
}