using API.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("prompt/template/")]
public class PromptTemplateController : ControllerBase
{
    [HttpPost("add/")]
    public IActionResult Create(PromptTemplateRequest pt)
    {
        return Ok();
        //AuthenticationUser
        //Handle pt
    }
    [HttpPut("update/")]
    public IActionResult Update(PromptTemplateRequest pt)
    {
        return Ok();
        //AuthenticationUser
        //Handle pt
    }
}