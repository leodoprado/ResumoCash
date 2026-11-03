using ResumoCash.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResumoCash.Application.Competences.GetAll;

public class GetCompetenceService
{
    private readonly ICompetenceRepository _competenceRepository;
    public GetCompetenceService(ICompetenceRepository competenceRepository)
    {
        _competenceRepository = competenceRepository;
    }

    public async Task<IEnumerable<GetCompetenceResponse>> ExecuteAsync(Guid userId)
    {
        var competences = await _competenceRepository.GetByUserIdAsync(userId);

        return competences.Select(competence =>
            new GetCompetenceResponse(
                competence.Id,
                competence.Month,
                competence.Status
            )
        );
    }
}
