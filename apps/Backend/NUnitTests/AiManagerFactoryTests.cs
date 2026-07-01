using Application.Models.AiModel;
using Infrastructure.AI;
using Infrastructure.AI.Options;
using Microsoft.Extensions.Options;

namespace NUnit;

[TestFixture]
public class AiManagerFactoryTests
{
    [Test]
    public void Create_WithGeminiModel20Flash_ReturnsGoogleAiManager()
    {
        var fakeOptions = new AiOptions
        {
            GEMINI_API_KEY = "fake-gemini-api-key"
        };
        
        IOptions<AiOptions> optionsWrapper = Options.Create(fakeOptions);

        var factory = new AiManagerFactory(optionsWrapper);

        var manager = factory.Create(AiModelTypes.Gemini20Flash);

        Assert.That(manager, Is.TypeOf<GoogleAiManager>());
        Assert.That(manager.Model.Type, Is.EqualTo(AiModelTypes.Gemini20Flash));
    }
    [Test]
    public void Create_WithOpenAIModel_ReturnsOpenAiManager()
    {
        var fakeOptions = new AiOptions
        {
            OPENAI_API_KEY = "fake-openapi-api-key"
        };
        
        IOptions<AiOptions> optionsWrapper = Options.Create(fakeOptions);

        var factory = new AiManagerFactory(optionsWrapper);

        var manager = factory.Create(AiModelTypes.Gpt4O);

        Assert.That(manager, Is.TypeOf<OpenAiManager>());
        Assert.That(manager.Model.Type, Is.EqualTo(AiModelTypes.Gpt4O));
    }
}