using OneOf;
using ResumoCash.Domain.Common;
using ResumoCash.Domain.Common.Errors;
using ResumoCash.Domain.Entities;
using ResumoCash.Domain.Repositories;
using Success = OneOf.Types.Success;

namespace ResumoCash.Application.Competences.Close;

public class CloseCompetenceService
{
    private readonly ICompetenceRepository _competenceRepository;

    public CloseCompetenceService(
        ICompetenceRepository competenceRepository)
    {
        _competenceRepository = competenceRepository;
    }

    public async Task<OneOf<Success, Error>> ExecuteAsync(
        Guid userId,
        Guid competenceId,
        CancellationToken cancellationToken = default)
    {
        var competence = await _competenceRepository.GetByIdAsync(
            userId,
            competenceId,
            cancellationToken
        );

        if (competence is null)
            return CompetenceErrors.NotFound;

        competence.Close();

        // Calcula o próximo mês
        var nextMonth = competence.Month.AddMonths(1);

        // Verifica se a próxima competência já existe
        var nextCompetenceExists = await _competenceRepository.ExistsAsync(
            userId,
            nextMonth,
            cancellationToken
        );

        if (!nextCompetenceExists)
        {
            var nextCompetence = new Competence(
                userId,
                nextMonth
            );

            await _competenceRepository.AddAsync(
                nextCompetence,
                cancellationToken
            );
        }

        await _competenceRepository.SaveChangesAsync(
            cancellationToken
        );

        return new Success();
    }
}
