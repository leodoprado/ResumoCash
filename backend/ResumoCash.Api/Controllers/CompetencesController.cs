using Microsoft.AspNetCore.Mvc;
using ResumoCash.Application.Competences.Close;
using ResumoCash.Application.Competences.Create;
using ResumoCash.Application.Competences.Reopen;

namespace ResumoCash.Api.Controllers;

[ApiController]
[Route("api/competences")]
public class CompetencesController : ControllerBase
{
    private readonly CreateCompetenceService _createCompetenceService;
    private readonly CloseCompetenceService _closeCompetenceService;
    private readonly ReopenCompetenceService _reopenCompetenceService;
    public CompetencesController(
        CreateCompetenceService createCompetenceService, 
        CloseCompetenceService closeCompetenceService, 
        ReopenCompetenceService reopenCompetenceService)
    {
        _createCompetenceService = createCompetenceService;
        _closeCompetenceService = closeCompetenceService;
        _reopenCompetenceService = reopenCompetenceService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCompetenceRequest request)
    {
        var userId = Guid.Parse(
            "11111111-1111-1111-1111-111111111111"
        );

        var result = await _createCompetenceService.ExecuteAsync(userId, request);
        
        return Created($"/api/competences/{result.Id}", result);
    }

    [HttpPatch("{id:guid}/close")]
    public async Task<IActionResult> Close(
        [FromRoute] 
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var userId = Guid.Parse(
            "11111111-1111-1111-1111-111111111111"
        );

        await _closeCompetenceService.ExecuteAsync(userId, id, cancellationToken);

        return NoContent();
    }

    [HttpPatch("{id:guid}/reopen")]
    public async Task<IActionResult> Reopen(
        [FromRoute]
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var userId = Guid.Parse(
            "11111111-1111-1111-1111-111111111111"
        );

        await _reopenCompetenceService.ExecuteAsync(userId, id, cancellationToken);

        return NoContent();
    }
}
