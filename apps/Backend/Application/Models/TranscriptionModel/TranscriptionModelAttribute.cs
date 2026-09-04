using Application.Models.TranscriptionProvider;

namespace Application.Models.TranscriptionModel;

public class TranscriptionModelAttribute(TranscriptionProviderTypes providerTypes) : Attribute
{
    public readonly TranscriptionProviderTypes ProviderTypes = providerTypes;
}