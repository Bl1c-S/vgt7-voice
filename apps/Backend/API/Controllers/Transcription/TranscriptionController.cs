using API.Models.DTOs;
using API.Models.Requests;
using Application.Models.Transcription;
using Application.Services.AI;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Transcription;

//[Authorize]?
[ApiController]
[Route("transcribe")]
public class TranscriptionController(DeepGramAiManager manager) : ControllerBase
{
    [HttpPost("transcribe")]
    public async Task<ActionResult<CallTranscriptResponse>> Transcribe(
        [FromForm] TranscribeCallRequest request)
    {
        if (request.Audio.Length == 0)
            return BadRequest("Audio file is empty.");

        long maxFileSize = 50 * 1024 * 1024; // 50 Mb? 
        if (request.Audio.Length > maxFileSize)
            return BadRequest("File size exceeds the 50MB limit.");


        await using var stream = request.Audio.OpenReadStream();
        using var memoryStream = new MemoryStream();
        await stream.CopyToAsync(memoryStream);
        var audioBytes = memoryStream.ToArray();

        ConversationTranscript transcript;
        try
        {
            transcript = await manager.SendRequestAsync(audioBytes, request.ManagerChannel);
        }
        catch (HttpRequestException ex)
        {
            return Problem(statusCode: 502, title: "Transcription service error", detail: ex.Message);
        }
        //todo badFormat handling (catch deepgram Ex)

        return Ok(MapToResponse(transcript));
    }

    private static CallTranscriptResponse MapToResponse(ConversationTranscript transcript) =>
        new(
            transcript.Conversation.Select(u => new UtteranceDto(u.Start, u.End, u.Role.ToString(), u.Text)).ToList(),
            transcript.DurationSec,
            transcript.FullText,
            transcript.Summary);
}