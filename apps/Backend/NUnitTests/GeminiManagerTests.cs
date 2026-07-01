using Application.Models.AiModel;
using Infrastructure.AI;

namespace NUnit;

[TestFixture]
public class GeminiManagerFlashModelTests : IDisposable
{
    private GoogleAiManager _manager;

    [OneTimeSetUp]
    public void SetUp()
    {
        var _realApiKey = Environment.GetEnvironmentVariable("GEMINI_API_KEY") ?? string.Empty;
        var descriptor = new AiModelDescriptor(AiModelTypes.Gemini20Flash);
        _manager = new GoogleAiManager(descriptor, _realApiKey);
    }

    [Test]
    public async Task SendRequestAsync_ReturnsNonEmptyResponse()
    {
        var result = await _manager.SendRequestAsync("Скажи просто: привет");
        //TODO Retry Policy for flash lite model

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.Not.Empty);
        Console.WriteLine($"Response: '{result}'");
        Dispose();
    }
    
    public void Dispose()
    {
        _manager.Dispose();
    }
}