using Application.Models.AiModel;
using Application.Services.AI;

namespace NUnit.TranscriptionTests;

[TestFixture]
[Explicit("Needs real API key and audioFile")]
public class DeepgramTranscriptionManagerIntegrationTest
{
    private const string TestAudioFilePath = "TestFiles/realistic_telephony_stereo.wav";

    [Test]
    public async Task TranscribeAsync_RealStereoCall_ReturnsCoherentTranscript()
    {
        var apiKey = Environment.GetEnvironmentVariable("DEEPGRAM_API_KEY");
        Assert.That(apiKey, Is.Not.Null.And.Not.Empty,
            "Needs API key.");

        Assert.That(File.Exists(TestAudioFilePath), Is.True,
            $"Test audio not found: {Path.GetFullPath(TestAudioFilePath)}");

        var model = new AiModelDescriptor(AiModelTypes.DeepgramNova3);
        var manager = new DeepGramAiManager(model, apiKey!);

        var audioBytes = await File.ReadAllBytesAsync(TestAudioFilePath);

        var result = await manager.SendRequestAsync(audioBytes, 0);

        Assert.That(result.Conversation, Is.Not.Empty);
        Assert.That(result.DurationSec, Is.GreaterThan(0));
        
        for (var i = 1; i < result.Conversation.Count; i++)
        {
            Assert.That(result.Conversation[i].Start, Is.GreaterThanOrEqualTo(result.Conversation[i - 1].Start));

        }
        
        Console.WriteLine($"Utterances count: {result.Conversation.Count}");

        Console.WriteLine("\n--- Utterances with Timestamps ---");
        foreach (var utt in result.Conversation)
        {
            Console.WriteLine($"[{utt.Start:F2}s - {utt.End:F2}s] (Speaker {utt.Role}): {utt.Text}");
        }

        Console.WriteLine("\n--- FullText ---");
        Console.WriteLine(result.FullText);
        
        Console.WriteLine($"Duration: {result.DurationSec} sec");
        Console.WriteLine($"Utterances count: {result.Conversation.Count}");
    }
}
