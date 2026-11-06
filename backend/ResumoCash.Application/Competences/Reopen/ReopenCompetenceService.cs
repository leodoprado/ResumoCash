using OneOf;
using ResumoCash.Domain.Common;
using ResumoCash.Domain.Common.Errors;
using ResumoCash.Domain.Repositories;
using Success = OneOf.Types.Success;

namespace ResumoCash.Application.Competences.Reopen;

public class ReopenCompetenceService
{
    private readonly ICompetenceRepository _competenceRepository;
    public ReopenCompetenceService(ICompetenceRepository competenceRepository)
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

        competence.Reopen();

        await _competenceRepository.SaveChangesAsync(
            cancellationToken
        );

        return new Success();
    }
}
