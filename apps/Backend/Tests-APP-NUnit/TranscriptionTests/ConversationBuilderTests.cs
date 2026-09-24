using Application.Models.Transcription;
using Application.Services.Transcription;

namespace NUnit.TranscriptionTests;

[TestFixture]
public class ConversationBuilderTests
{
    [Test]
    public void Build_EmptyWords_ReturnsEmptyList()
    {
        var builder = new ConversationBuilder(new List<TranscribedWord>(), channel: 0, managerChannel: 0);
        var segments = builder.Build();

        Assert.That(segments, Is.Empty);
    }

    [Test]
    public void Build_SingleWord_ReturnsSingleSegmentWithCorrectProperties()
    {
        var words = new List<TranscribedWord>
        {
            new(0, 1, "Привет", 1.0m, 1.5m)
        };

        var builder = new ConversationBuilder(words, channel: 0, managerChannel: 0);
        var segments = builder.Build();

        Assert.That(segments, Has.Count.EqualTo(1));
        var segment = segments[0];
        Assert.That(segment.Start, Is.EqualTo(1.0m));
        Assert.That(segment.End, Is.EqualTo(1.5m));
        Assert.That(segment.Channel, Is.EqualTo(0));
        Assert.That(segment.Speaker, Is.EqualTo(1));
        Assert.That(segment.Role, Is.EqualTo(InterlocutorTypes.Manager));
        Assert.That(segment.Text, Is.EqualTo("Привет"));
    }

    [Test]
    public void Build_MultipleWordsUnderThreshold_MergesIntoOneSegment()
    {
        var words = new List<TranscribedWord>
        {
            new(0, 0, "Добрый", 0.0m, 0.5m),
            new(0, 0, "день", 0.6m, 1.0m),
            new(0, 0, "всем", 1.2m, 1.8m),
        };

        var builder = new ConversationBuilder(words, channel: 0, managerChannel: 0);
        var segments = builder.Build();

        Assert.That(segments, Has.Count.EqualTo(1));
        Assert.That(segments[0].Start, Is.EqualTo(0.0m));
        Assert.That(segments[0].End, Is.EqualTo(1.8m));
        Assert.That(segments[0].Text, Is.EqualTo("Добрый день всем"));
    }

    [Test]
    public void Build_PauseGreaterThanThreshold_SplitsSegments()
    {
        var words = new List<TranscribedWord>
        {
            new(0, 0, "Первая", 0.0m, 1.0m),
            new(0, 0, "фраза", 1.2m, 2.0m),
            new(0, 0, "Вторая", 3.0m, 3.5m), // pause = 3.0 - 2.0 = 1.0 > 0.8
            new(0, 0, "фраза", 3.6m, 4.0m)
        };

        var builder = new ConversationBuilder(words, channel: 0, managerChannel: 0);
        var segments = builder.Build();

        Assert.That(segments, Has.Count.EqualTo(2));
        Assert.That(segments[0].Text, Is.EqualTo("Первая фраза"));
        Assert.That(segments[0].Start, Is.EqualTo(0.0m));
        Assert.That(segments[0].End, Is.EqualTo(2.0m));

        Assert.That(segments[1].Text, Is.EqualTo("Вторая фраза"));
        Assert.That(segments[1].Start, Is.EqualTo(3.0m));
        Assert.That(segments[1].End, Is.EqualTo(4.0m));
    }

    [Test]
    public void Build_SpeakerChanged_SplitsSegments()
    {
        var words = new List<TranscribedWord>
        {
            new(0, 0, "Говорит", 0.0m, 0.5m),
            new(0, 0, "первый", 0.5m, 1.0m),
            new(0, 1, "Говорит", 1.1m, 1.5m),
            new(0, 1, "второй", 1.5m, 2.0m)
        };

        var builder = new ConversationBuilder(words, channel: 0, managerChannel: 0);
        var segments = builder.Build();

        Assert.That(segments, Has.Count.EqualTo(2));
        Assert.That(segments[0].Speaker, Is.EqualTo(0));
        Assert.That(segments[0].Text, Is.EqualTo("Говорит первый"));
        Assert.That(segments[1].Speaker, Is.EqualTo(1));
        Assert.That(segments[1].Text, Is.EqualTo("Говорит второй"));
    }

    [Test]
    public void Build_RoleResolution_AssignsManagerAndClientRoles()
    {
        var words = new List<TranscribedWord> { new(1, 0, "Тест", 0.0m, 1.0m) };

        var managerBuilder = new ConversationBuilder(words, channel: 1, managerChannel: 1);
        var clientBuilder = new ConversationBuilder(words, channel: 1, managerChannel: 0);
        var unknownBuilder = new ConversationBuilder(words, channel: 2, managerChannel: 0);

        Assert.That(managerBuilder.Build()[0].Role, Is.EqualTo(InterlocutorTypes.Manager));
        Assert.That(clientBuilder.Build()[0].Role, Is.EqualTo(InterlocutorTypes.Client));
        Assert.That(unknownBuilder.Build()[0].Role, Is.EqualTo(InterlocutorTypes.Unknown));
    }
}
