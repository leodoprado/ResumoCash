using ResumoCash.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResumoCash.Application.Competences.Reopen;

public class ReopenCompetenceService
{
    private readonly ICompetenceRepository _competenceRepository;
    public ReopenCompetenceService(ICompetenceRepository competenceRepository)
    {
        _competenceRepository = competenceRepository;
    }

    public async Task ExecuteAsync(
        Guid userId,
        Guid competenceId,
        CancellationToken cancellationToken = default)
    {
        var competence = await _competenceRepository.GetByIdAsync(
            userId,
            competenceId,
            cancellationToken
        );

        if (competence is null || competence.UserId != userId)
        {
            throw new InvalidOperationException(
                "Competência não encontrada."
            );
        }

        competence.Reopen();

        await _competenceRepository.SaveChangesAsync(
            cancellationToken
        );
    }
}
