using System.Text;
using Application.Models.Transcription;

namespace Application.Services.Transcription.Helpers;

public static class CallTranscriptBuilder
{
    private const decimal MaxPauseBetweenWordsSec = 0.8m; //TODO config/settings in plugin 

    public static CallTranscript Build(List<TranscribedWord> words, double callDurationSec, int managerChannel = 0)
    {
        if (words.Count == 0)
            return new CallTranscript { Utterances = [], DurationSec = callDurationSec };

        var validWords = words.Where(w => w is { Start: not null, End: not null }).ToList();
        
        var utterances = validWords
            .GroupBy(w => w.Channel)
            .SelectMany(channelGroup => MergeOneChannelIntoUtterances(
                channelGroup.OrderBy(w => w.Start!.Value).ToList(),
                channelGroup.Key,
                managerChannel))
            .OrderBy(u => u.Start) 
            .ToList();

        return new CallTranscript { Utterances = utterances, DurationSec = callDurationSec };
    }
    
    private static List<Utterance> MergeOneChannelIntoUtterances(
        List<TranscribedWord> channelWords, int channel, int managerChannel)
    {
        var utterances = new List<Utterance>();
        if (channelWords.Count == 0) return utterances;

        var textBuilder = new StringBuilder();
        var currentStart = channelWords[0].Start!.Value;
        var currentEnd = channelWords[0].End!.Value;
        var currentSpeaker = channelWords[0].Speaker;

        foreach (var word in channelWords)
        {
            var wordStart = word.Start!.Value;
            var wordEnd = word.End!.Value;

            var isSpeakerChanged = word.Speaker != currentSpeaker;
            var isPauseTooLong = wordStart - currentEnd > MaxPauseBetweenWordsSec;

            if (textBuilder.Length > 0 && (isSpeakerChanged || isPauseTooLong))
            {
                utterances.Add(BuildUtterance(channel, currentSpeaker, currentStart, currentEnd, textBuilder, managerChannel));

                textBuilder.Clear();
                currentStart = wordStart;
                currentSpeaker = word.Speaker;
            }

            textBuilder.Append(word.Text).Append(' ');
            currentEnd = wordEnd;
        }

        utterances.Add(BuildUtterance(channel, currentSpeaker, currentStart, currentEnd, textBuilder, managerChannel));

        return utterances;
    }

    private static Utterance BuildUtterance(
        int channel, int? speaker, decimal start, decimal end, StringBuilder textBuilder, int managerChannel)
    {
        return new Utterance
        {
            Start = start,
            End = end,
            Channel = channel,
            Speaker = speaker,
            Role = ResolveRole(channel, managerChannel),
            Text = textBuilder.ToString().TrimEnd()
        };
    }

    private static CallRole ResolveRole(int channel, int managerChannel)
    {
        if (channel < 0 || channel > 1) return CallRole.Unknown;
        return channel == managerChannel ? CallRole.Manager : CallRole.Client;
    }
}