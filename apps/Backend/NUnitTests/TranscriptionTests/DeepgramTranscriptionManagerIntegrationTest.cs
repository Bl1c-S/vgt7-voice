using Application.Models.TranscriptionModel;
using Application.Services.Transcription.Deepgram;

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

        var model = new TranscriptionModelDescriptor(TranscriptionModelTypes.DeepgramNova3);
        var manager = new DeepgramManager(model, apiKey!);

        var audioBytes = await File.ReadAllBytesAsync(TestAudioFilePath);

        var result = await manager.TranscribeAsync(audioBytes);

        Assert.That(result.Utterances, Is.Not.Empty);
        Assert.That(result.DurationSec, Is.GreaterThan(0));
        
        for (var i = 1; i < result.Utterances.Count; i++)
        {
            Assert.That(result.Utterances[i].Start, Is.GreaterThanOrEqualTo(result.Utterances[i - 1].Start));

        }
        
        Console.WriteLine($"Utterances count: {result.Utterances.Count}");

        Console.WriteLine("\n--- Utterances with Timestamps ---");
        foreach (var utt in result.Utterances)
        {
            Console.WriteLine($"[{utt.Start:F2}s - {utt.End:F2}s] (Speaker {utt.Role}): {utt.Text}");
        }

        Console.WriteLine("\n--- FullText ---");
        Console.WriteLine(result.FullText);
        
        Console.WriteLine($"Duration: {result.DurationSec} sec");
        Console.WriteLine($"Utterances count: {result.Utterances.Count}");
    }
}
