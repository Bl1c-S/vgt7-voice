using Application.Models.Transcription;
using Deepgram.Models.Listen.v1.REST;

namespace Application.Services.Transcription.Deepgram;

public static class DeepgramResponseMapper
{
    public static List<TranscribedWord> MapToWords(IReadOnlyList<Channel> channels)
    {
        var words = new List<TranscribedWord>();
        for (var channelIndex = 0; channelIndex < channels.Count; channelIndex++)
        {
            var alternative = channels[channelIndex].Alternatives?.FirstOrDefault();
            if (alternative?.Words is null) continue;

            foreach (var word in alternative.Words)
            {
                words.Add(new TranscribedWord(
                    channelIndex, word.Speaker,
                    word.PunctuatedWord ?? word.HeardWord ?? "",
                    word.Start, word.End));
            }
        }
        return words;
    }
}