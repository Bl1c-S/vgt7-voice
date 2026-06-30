using Application.Models.AiProvider;

namespace Application.Models.AiModel;

public class AiModelAttribute(AiProviderTypes providerTypes) : Attribute
{
    public readonly AiProviderTypes ProviderTypes = providerTypes;
}