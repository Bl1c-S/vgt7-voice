using Application.Models.TranscriptionProvider;

namespace Application.Models.TranscriptionModel;
using Application.Models.Extensions;

public class TranscriptionModelDescriptor
{
    public TranscriptionModelTypes Type
    {
        get;
        set
        {
            if (value.GetTranscriptionProvider() == Provider)
                field = value;
            else
                throw new ArgumentException("Model provider mismatch");
        }
    }

    public string Name => Type.GetDescription();
    
    public readonly TranscriptionProviderTypes Provider;
    
    public TranscriptionModelDescriptor(TranscriptionModelTypes type)
    {
        Provider = type.GetTranscriptionProvider();
        Type = type;
    }
}