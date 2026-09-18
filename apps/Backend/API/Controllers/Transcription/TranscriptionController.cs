using API.Models.DTOs;
using API.Models.Requests;
using Application.Models.Transcription;
using Application.Services.Transcription;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Transcription;

//[Authorize]?
[ApiController]
[Route("api/calls")]
public class TranscriptionController(TranscriptionManagerFactory factory) : ControllerBase
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

        //TODO modelType validation
        var manager = factory.Create(request.Model);

        CallTranscript transcript;
        try
        {
            transcript = await manager.TranscribeAsync(audioBytes, request.ManagerChannel);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(502, new { message = "Transcription service error", detail = ex.Message });
        }
        //todo badFormat handling (catch deepgram Ex)

        return Ok(MapToResponse(transcript));
    }

    private static CallTranscriptResponse MapToResponse(CallTranscript transcript) =>
        new(
            transcript.Utterances.Select(u => new UtteranceDto(u.Start, u.End, u.Role.ToString(), u.Text)).ToList(),
            transcript.DurationSec,
            transcript.FullText,
            transcript.Summary);
}