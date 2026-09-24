using Application.Models.Transcription;

namespace Application.Services.Transcription;

public static class ConversationTranscriptBuilder
{
    public static ConversationTranscript Build(List<TranscribedWord> words, double duration, int managerChannel = 0)
    {
        if (words.Count == 0)
            return new ConversationTranscript([], duration);

        var validWords = words.Where(w => w is { Start: not null, End: not null }).ToList();
        
        var utterances = validWords
            .GroupBy(w => w.Channel)
            .SelectMany(channelGroup => MergeOneChannelIntoConversation(
                channelGroup.OrderBy(w => w.Start!.Value).ToList(),
                channelGroup.Key,
                managerChannel))
            .OrderBy(u => u.Start) 
            .ToList();
        
        return new ConversationTranscript(utterances, duration);
    }
    
    private static List<ConversationSegment> MergeOneChannelIntoConversation(
        IReadOnlyList<TranscribedWord> channelWords, int channel, int managerChannel)
    {
        return channelWords.Count == 0 ? [] : new ConversationBuilder(channelWords, channel, managerChannel).Build();
    }
}