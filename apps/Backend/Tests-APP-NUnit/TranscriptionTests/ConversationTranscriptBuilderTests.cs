using Application.Models.Transcription;
using Application.Services.Transcription;

namespace NUnit.TranscriptionTests;

[TestFixture]
public class ConversationTranscriptBuilderTests
{
    [Test]
    public void Build_EmptyWords_ReturnsEmptyConversation()
    {
        var result = ConversationTranscriptBuilder.Build(new List<TranscribedWord>(), duration: 0);

        Assert.That(result.Conversation, Is.Empty);
        Assert.That(result.DurationSec, Is.EqualTo(0));
        Assert.That(result.FullText, Is.Empty);
    }

    [Test]
    public void Build_SingleChannelSingleSpeaker_ReturnsOneSegment()
    {
        var words = new List<TranscribedWord>
        {
            new(0, 0, "Добрый", 0.0m, 0.5m),
            new(0, 0, "день", 0.5m, 1.0m),
        };

        var result = ConversationTranscriptBuilder.Build(words, duration: 1.0);

        Assert.That(result.Conversation, Has.Count.EqualTo(1));
        Assert.That(result.Conversation[0].Text, Is.EqualTo("Добрый день"));
        Assert.That(result.Conversation[0].Role, Is.EqualTo(InterlocutorTypes.Manager));
        Assert.That(result.FullText, Is.EqualTo("Manager: Добрый день"));
    }

    [Test]
    public void Build_CrossTalk_DoesNotSplitMonologue()
    {
        var words = new List<TranscribedWord>
        {
            new(0, 0, "Добрый", 0.0m, 0.5m),
            new(1, 0, "Да", 0.4m, 0.6m),
            new(0, 0, "день", 0.5m, 0.8m),
            new(0, 0, "меня", 0.8m, 1.0m),
            new(0, 0, "зовут", 1.0m, 1.2m),
            new(0, 0, "Иван", 1.2m, 1.5m),
        };

        var result = ConversationTranscriptBuilder.Build(words, duration: 1.5);

        var managerSegment = result.Conversation.Single(u => u.Role == InterlocutorTypes.Manager);
        Assert.That(managerSegment.Text, Is.EqualTo("Добрый день меня зовут Иван"));

        var clientSegment = result.Conversation.Single(u => u.Role == InterlocutorTypes.Client);
        Assert.That(clientSegment.Text, Is.EqualTo("Да"));

        Assert.That(result.Conversation, Has.Count.EqualTo(2));
    }

    [Test]
    public void Build_PauseExceedsThreshold_SplitsSegment()
    {
        var words = new List<TranscribedWord>
        {
            new(0, 0, "Минутку", 10.0m, 10.5m),
            new(0, 0, "я", 10.5m, 10.7m),
            new(0, 0, "проверю", 10.7m, 11.0m),
            new(0, 0, "Я", 50.0m, 50.2m),
            new(0, 0, "нашел", 50.2m, 50.5m),
            new(0, 0, "данные", 50.5m, 51.0m),
        };

        var result = ConversationTranscriptBuilder.Build(words, duration: 51.0);

        Assert.That(result.Conversation, Has.Count.EqualTo(2));
        Assert.That(result.Conversation[0].Text, Is.EqualTo("Минутку я проверю"));
        Assert.That(result.Conversation[0].End, Is.EqualTo(11.0m));
        Assert.That(result.Conversation[1].Text, Is.EqualTo("Я нашел данные"));
        Assert.That(result.Conversation[1].Start, Is.EqualTo(50.0m));
    }

    [Test]
    public void Build_PauseWithinThreshold_DoesNotSplit()
    {
        var words = new List<TranscribedWord>
        {
            new(0, 0, "Слово1", 0.0m, 0.5m),
            new(0, 0, "Слово2", 1.1m, 1.6m),
        };

        var result = ConversationTranscriptBuilder.Build(words, duration: 2.0);

        Assert.That(result.Conversation, Has.Count.EqualTo(1));
        Assert.That(result.Conversation[0].Text, Is.EqualTo("Слово1 Слово2"));
    }

    [Test]
    public void Build_SpeakerChangeWithinSameChannel_SplitsSegment()
    {
        var words = new List<TranscribedWord>
        {
            new(0, 0, "Приемная", 0.0m, 0.5m),
            new(0, 1, "Дарья", 5.0m, 5.5m),
        };

        var result = ConversationTranscriptBuilder.Build(words, duration: 5.5);

        Assert.That(result.Conversation, Has.Count.EqualTo(2));
        Assert.That(result.Conversation[0].Speaker, Is.EqualTo(0));
        Assert.That(result.Conversation[1].Speaker, Is.EqualTo(1));
        Assert.That(result.Conversation[0].Role, Is.EqualTo(InterlocutorTypes.Manager));
        Assert.That(result.Conversation[1].Role, Is.EqualTo(InterlocutorTypes.Manager));
    }

    [Test]
    public void Build_WordsWithoutTimestamps_AreFilteredOut()
    {
        var words = new List<TranscribedWord>
        {
            new(0, 0, "Добрый", 0.0m, 0.5m),
            new(0, 0, "непонятное", null, null), 
            new(0, 0, "день", 0.5m, 1.0m),
        };

        var result = ConversationTranscriptBuilder.Build(words, duration: 1.0);

        Assert.That(result.Conversation, Has.Count.EqualTo(1));
        Assert.That(result.Conversation[0].Text, Is.EqualTo("Добрый день"));
    }

    [Test]
    public void Build_AllWordsWithoutTimestamps_ReturnsEmptyConversation()
    {
        var words = new List<TranscribedWord>
        {
            new(0, 0, "тест", null, null),
        };

        var result = ConversationTranscriptBuilder.Build(words, duration: 1.0);

        Assert.That(result.Conversation, Is.Empty);
    }

    [Test]
    public void Build_CustomManagerChannel_AssignsRolesCorrectly()
    {
        var words = new List<TranscribedWord>
        {
            new(0, 0, "Здравствуйте", 0.0m, 0.5m),
            new(1, 0, "Добрый день", 0.5m, 1.0m),
        };

        var result = ConversationTranscriptBuilder.Build(words, duration: 1.0, managerChannel: 1);

        Assert.That(result.Conversation[0].Role, Is.EqualTo(InterlocutorTypes.Client));
        Assert.That(result.Conversation[1].Role, Is.EqualTo(InterlocutorTypes.Manager));
    }

    [Test]
    public void Build_ChannelOutsideZeroOne_AssignsUnknownRole()
    {
        var words = new List<TranscribedWord>
        {
            new(2, 0, "Алло", 0.0m, 0.5m),
        };

        var result = ConversationTranscriptBuilder.Build(words, duration: 0.5);

        Assert.That(result.Conversation[0].Role, Is.EqualTo(InterlocutorTypes.Unknown));
    }

    [Test]
    public void Build_SegmentsAreOrderedByStartTime_AcrossChannels()
    {
        var words = new List<TranscribedWord>
        {
            new(1, 0, "второй", 2.0m, 2.5m),
            new(0, 0, "первый", 0.0m, 0.5m),
        };

        var result = ConversationTranscriptBuilder.Build(words, duration: 2.5);

        Assert.That(result.Conversation[0].Text, Is.EqualTo("первый"));
        Assert.That(result.Conversation[1].Text, Is.EqualTo("второй"));
    }
}