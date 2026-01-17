using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace InteractiveWhiteboard_back.Controllers.Stroke.Create;

[ApiController]
[Route("v{version:apiVersion}/stroke/create")]
[ApiVersion(1.0)]
public class CreateStrokeController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateStrokeAsync(CreateStrokeRequest req, CancellationToken ct)
    {

        return Created();
    }
}