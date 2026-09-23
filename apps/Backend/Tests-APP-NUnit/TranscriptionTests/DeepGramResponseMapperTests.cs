using Application.Services.Transcription.Deepgram;
using Deepgram.Models.Listen.v1.REST;

namespace NUnit.TranscriptionTests;

[TestFixture]
public class DeepGramResponseMapperTests
{
    [Test]
    public void MapToWords_EmptyChannels_ReturnsEmptyList()
    {
        var result = DeepGramResponseMapper.MapToWords([]);

        Assert.That(result, Is.Empty);
    }

    [Test]
    public void MapToWords_ChannelWithNoAlternatives_SkipsChannel()
    {
        var channels = new List<Channel>
        {
            new() { Alternatives = null }
        };

        var result = DeepGramResponseMapper.MapToWords(channels);

        Assert.That(result, Is.Empty);
    }

    [Test]
    public void MapToWords_ChannelWithNullWords_SkipsChannel()
    {
        var channels = new List<Channel>
        {
            new()
            {
                Alternatives = new List<Alternative>
                {
                    new() { Words = null }
                }
            }
        };

        var result = DeepGramResponseMapper.MapToWords(channels);

        Assert.That(result, Is.Empty);
    }

    [Test]
    public void MapToWords_ValidChannelsAndWords_MapsCorrectly()
    {
        var channels = new List<Channel>
        {
            new()
            {
                Alternatives = new List<Alternative>
                {
                    new()
                    {
                        Words = new List<Word>
                        {
                            new()
                            {
                                PunctuatedWord = "Привет,",
                                HeardWord = "привет",
                                Start = 0.0m,
                                End = 0.5m,
                                Speaker = 0
                            },
                            new()
                            {
                                PunctuatedWord = null,
                                HeardWord = "мир",
                                Start = 0.5m,
                                End = 1.0m,
                                Speaker = 0
                            }
                        }
                    }
                }
            },
            new()
            {
                Alternatives = new List<Alternative>
                {
                    new()
                    {
                        Words = new List<Word>
                        {
                            new()
                            {
                                PunctuatedWord = "Здравствуйте",
                                HeardWord = "здравствуйте",
                                Start = 0.2m,
                                End = 0.8m,
                                Speaker = 1
                            }
                        }
                    }
                }
            }
        };

        var result = DeepGramResponseMapper.MapToWords(channels);

        Assert.That(result, Has.Count.EqualTo(3));
        
        Assert.That(result[0].Channel, Is.EqualTo(0));
        Assert.That(result[0].Speaker, Is.EqualTo(0));
        Assert.That(result[0].Text, Is.EqualTo("Привет,"));
        Assert.That(result[0].Start, Is.EqualTo(0.0m));
        Assert.That(result[0].End, Is.EqualTo(0.5m));

        Assert.That(result[1].Channel, Is.EqualTo(0));
        Assert.That(result[1].Speaker, Is.EqualTo(0));
        Assert.That(result[1].Text, Is.EqualTo("мир"));
        Assert.That(result[1].Start, Is.EqualTo(0.5m));
        Assert.That(result[1].End, Is.EqualTo(1.0m));

        Assert.That(result[2].Channel, Is.EqualTo(1));
        Assert.That(result[2].Speaker, Is.EqualTo(1));
        Assert.That(result[2].Text, Is.EqualTo("Здравствуйте"));
        Assert.That(result[2].Start, Is.EqualTo(0.2m));
        Assert.That(result[2].End, Is.EqualTo(0.8m));
    }
}
