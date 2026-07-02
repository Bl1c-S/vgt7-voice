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
        var model = new AiModelDescriptor(AiModelTypes.Gemini20Flash);
        _manager = new GoogleAiManager(model, "GEMINI_API_KEY");
    }

    [Test]
    public async Task SendRequestAsync_ReturnsNonEmptyResponse()
    {
        var response = await _manager.SendRequestAsync("тут ошибка");
        //TODO Retry Policy for flash lite model

        Assert.That(response, Is.Not.Null);
        Assert.That(response, Is.Not.Empty);
        Console.WriteLine($"Response: '{response}'");
        Dispose();
    }

    public void Dispose()
    {
        _manager.Dispose();
    }
}