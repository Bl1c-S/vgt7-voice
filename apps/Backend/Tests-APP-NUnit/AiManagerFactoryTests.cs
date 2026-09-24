using Application.Models.AiModel;
using Application.Services.AI;
using Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace NUnit;

[TestFixture]
public class AiManagerFactoryTests
{
    private readonly AiManagerFactory _factory;

    public AiManagerFactoryTests()
    {
        var aiOptions = new AiOptions
        {
            GeminiApiKey = "fake-gemini-api-key",
            OpenaiApiKey = "fake-openapi-api-key",
            DeepgramApiKey = "fake-deepgram-api-key"
        };
        var optionsWrapper = Options.Create(aiOptions);
        
        _factory = new AiManagerFactory(optionsWrapper);
    }
    
    [Test]
    public void Create_WithGeminiModel20Flash_ReturnsGoogleAiManager()
    {
        var manager = _factory.Create(AiModelTypes.Gemini20Flash);

        Assert.That(manager, Is.TypeOf<GoogleAiManager>());
        Assert.That(manager.Model.Type, Is.EqualTo(AiModelTypes.Gemini20Flash));
    }
    [Test]
    public void Create_WithOpenAIModel_ReturnsOpenAiManager()
    {
        var manager = _factory.Create(AiModelTypes.Gpt4O);

        Assert.That(manager, Is.TypeOf<OpenAiManager>());
        Assert.That(manager.Model.Type, Is.EqualTo(AiModelTypes.Gpt4O));
    }
    [Test]
    public void Create_WithDeepgramNova3_ReturnsDeepGramAiManager()
    {
        var manager = _factory.Create(AiModelTypes.DeepgramNova3);

        Assert.That(manager, Is.TypeOf<DeepGramAiManager>());
        Assert.That(manager.Model.Type, Is.EqualTo(AiModelTypes.DeepgramNova3));
    }
}