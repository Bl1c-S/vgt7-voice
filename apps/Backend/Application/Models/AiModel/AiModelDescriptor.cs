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
        Type = type;
        Name = type.GetDescription();
        Provider = type.GetAiProvider();
        ApiKeyConfigName = Provider.GetDescription();
    }
}