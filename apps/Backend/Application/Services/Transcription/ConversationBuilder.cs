using System.Text;
using Application.Models.Transcription;

namespace Application.Services.Transcription;

public class ConversationBuilder(IReadOnlyList<TranscribedWord> words, int channel, int managerChannel)
{
    private const decimal MaxPauseBetweenWordsSec = 0.8m; //TODO config/settings in plugin 

    private readonly List<ConversationSegment> _segments = new();
    private readonly StringBuilder _textBuilder = new();

    private decimal _start;
    private decimal _end;
    private int? _speaker;

    public List<ConversationSegment> Build()
    {
        if (words.Count == 0)
            return _segments;

        InitializeFirstWord();

        for (var i = 1; i < words.Count; i++)
            ProcessWord(words[i]);

        FinalizeSegment();
        return _segments;
    }

    private void ProcessWord(TranscribedWord word)
    {
        var wordStart = word.Start!.Value;

        var isSpeakerChanged = word.Speaker != _speaker;
        var isPauseTooLong = wordStart - _end > MaxPauseBetweenWordsSec;

        if (_textBuilder.Length > 0 && (isSpeakerChanged || isPauseTooLong))
        {
            FinalizeSegment();
            _start = wordStart;
            _speaker = word.Speaker;
        }

        _textBuilder.Append(word.Text).Append(' ');
        _end = word.End!.Value;
    }

    private void InitializeFirstWord()
    {
        var firstWord = words[0];
        _start = firstWord.Start!.Value;
        _end = firstWord.End!.Value;
        _speaker = firstWord.Speaker;
        _textBuilder.Append(firstWord.Text).Append(' ');
    }

    private void FinalizeSegment()
    {
        _segments.Add(new ConversationSegment
        {
            Start = _start,
            End = _end,
            Channel = channel,
            Speaker = _speaker,
            Role = ResolveRole(channel, managerChannel),
            Text = _textBuilder.ToString().TrimEnd()
        });

        _textBuilder.Clear();
    }

    private static InterlocutorTypes ResolveRole(int channel, int managerChannel)
    {
        if (channel < 0 || channel > 1) return InterlocutorTypes.Unknown;
        return channel == managerChannel ? InterlocutorTypes.Manager : InterlocutorTypes.Client;
    }
}