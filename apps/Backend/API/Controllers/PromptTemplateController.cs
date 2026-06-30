using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("wh/pt/")]
public class PromptTemplateController : ControllerBase
{
    [HttpPost]
    public IActionResult Add(PromptTemplate pt)
    {
        return Ok();
    }
}