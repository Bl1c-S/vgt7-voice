using Application.Models.AiModel;
using Infrastructure.AI;
using Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace NUnit;

[TestFixture]
public class AiManagerFactoryTests
{
    private readonly AiManagerFactory _factory;

    private AiManagerFactoryTests()
    {
        var options = Options.Create(new AiOptions
        {
            GeminiApiKey = "fake-gemini-api-key",
            OpenaiApiKey = "fake-openapi-api-key"
        });
        _factory = new AiManagerFactory(options);
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
}