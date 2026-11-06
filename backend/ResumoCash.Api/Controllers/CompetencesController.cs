using Microsoft.AspNetCore.Mvc;
using ResumoCash.Api.Common.Extensions;
using ResumoCash.Application.Competences.Close;
using ResumoCash.Application.Competences.Create;
using ResumoCash.Application.Competences.GetAll;
using ResumoCash.Application.Competences.Reopen;

namespace ResumoCash.Api.Controllers;

[ApiController]
[Route("api/competences")]
public class CompetencesController : ControllerBase
{
    private readonly CreateCompetenceService _createCompetenceService;
    private readonly CloseCompetenceService _closeCompetenceService;
    private readonly ReopenCompetenceService _reopenCompetenceService;
    private readonly GetCompetenceService _getCompetenceService;
    public CompetencesController(
        CreateCompetenceService createCompetenceService, 
        CloseCompetenceService closeCompetenceService, 
        ReopenCompetenceService reopenCompetenceService,
        GetCompetenceService getCompetenceService)
    {
        _createCompetenceService = createCompetenceService;
        _closeCompetenceService = closeCompetenceService;
        _reopenCompetenceService = reopenCompetenceService;
        _getCompetenceService = getCompetenceService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCompetenceRequest request, CancellationToken cancellationToken = default)
    {
        var userId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var competenceResult = await _createCompetenceService.ExecuteAsync(userId, request, cancellationToken);

        if (competenceResult.IsError())
            return competenceResult.GetErrorResult().ToActionResult(this);
            
        var competence = competenceResult.GetSuccessResult();

        return Created($"/api/competences/{competence.Id}", competence);
    }

    [HttpPatch("{id:guid}/close")]
    public async Task<IActionResult> Close([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var userId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var competenceResult = await _closeCompetenceService.ExecuteAsync(userId, id, cancellationToken);

        if (competenceResult.IsError())
            return competenceResult.GetErrorResult().ToActionResult(this);

        return NoContent();
    }

    [HttpPatch("{id:guid}/reopen")]
    public async Task<IActionResult> Reopen([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var userId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var competenceResult = await _reopenCompetenceService.ExecuteAsync(userId, id, cancellationToken);

        if (competenceResult.IsError())
            return competenceResult.GetErrorResult().ToActionResult(this);

        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var userId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        
        var result = await _getCompetenceService.ExecuteAsync(userId);

        return Ok(result);
    }

}
