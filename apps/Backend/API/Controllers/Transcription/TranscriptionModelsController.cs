using API.Models.DTOs;
using Application.Models.Extensions;
using Application.Models.TranscriptionModel;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Transcription;

[ApiController]
[Route("api/transcription-models")]
public class TranscriptionModelsController : ControllerBase
{
    [HttpGet]
    public ActionResult<List<TranscriptionModelResponse>> GetAll()
    {
        var models = Enum.GetValues<TranscriptionModelTypes>()
            .Select(type => new TranscriptionModelResponse(
                Type: type.ToString(),
                Name: type.GetDescription(),
                Provider: type.GetTranscriptionProvider().ToString()))
            .ToList();

        return Ok(models);
    }
}

