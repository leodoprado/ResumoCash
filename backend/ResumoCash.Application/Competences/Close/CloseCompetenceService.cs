using ResumoCash.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResumoCash.Application.Competences.Close;

public class CloseCompetenceService
{
    private readonly ICompetenceRepository _competenceRepository;
    public CloseCompetenceService(ICompetenceRepository competenceRepository)
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

        competence.Close();
        
        await _competenceRepository.SaveChangesAsync(
            cancellationToken
        );
    }
}
