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
                throw new ArgumentException("Model provider mismatch");
        }
    }

    public string Name => Type.GetDescription();
    
    public readonly AiProviderTypes Provider;
    
    public AiModelDescriptor(AiModelTypes type)
    {
        Provider = type.GetAiProvider();
        Type = type;
    }
}