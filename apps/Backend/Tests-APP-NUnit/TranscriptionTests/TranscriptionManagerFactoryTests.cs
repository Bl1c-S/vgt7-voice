using Application.Models.TranscriptionModel;
using Application.Options;
using Application.Services.AI;
using Application.Services.Transcription;
using Application.Services.Transcription.Deepgram;
using Microsoft.Extensions.Options;

namespace NUnit.TranscriptionTests;

[TestFixture]
public class TranscriptionManagerFactoryTests
{
    private readonly TranscriptionManagerFactory _factory;

    public TranscriptionManagerFactoryTests()
    {
        var transcriptionOptions = new TranscriptionOptions
        {
            DeepgramApiKey = "fake-deepgram-api-key"
        };
        var optionsWrapper = Options.Create(transcriptionOptions);
        
        _factory = new TranscriptionManagerFactory(optionsWrapper);
    }
    
    [Test]
    public void Create_WithDeepgramNova3_ReturnsDeepgramManager()
    {
        var manager = _factory.Create(TranscriptionModelTypes.DeepgramNova3);

        Assert.That(manager, Is.TypeOf<DeepgramManager>());
        Assert.That(manager.Model.Type, Is.EqualTo(TranscriptionModelTypes.DeepgramNova3));
    }
   
}