using OneOf;
using ResumoCash.Domain.Common;
using ResumoCash.Domain.Common.Errors;
using ResumoCash.Domain.Entities;
using ResumoCash.Domain.Repositories;

namespace ResumoCash.Application.Competences.Create;

public class CreateCompetenceService
{
    private readonly ICompetenceRepository _competenceRepository;

    public CreateCompetenceService(
        ICompetenceRepository competenceRepository)
    {
        _competenceRepository = competenceRepository;
    }

    public async Task<OneOf<CreateCompetenceResponse, AppError>> ExecuteAsync(
        Guid userId,
        CreateCompetenceRequest request,
        CancellationToken cancellationToken = default)
    {
        var openCompetence = await _competenceRepository.GetOpenAsync(userId, cancellationToken);

        if (openCompetence is not null)
            return CompetenceErrors.AlreadyOpen;

        var month = new DateOnly(request.Month.Year, request.Month.Month, 1);

        var competences = await _competenceRepository
            .GetByUserIdAsync(userId, cancellationToken);

        if (competences.Any(x => x.Month == month))
            return CompetenceErrors.AlreadyExists;

        var competence = new Competence(userId, month);

        await _competenceRepository.AddAsync(
            competence,
            cancellationToken
        );

        await _competenceRepository.SaveChangesAsync(cancellationToken);

        return new CreateCompetenceResponse(
            competence.Id,
            competence.Month,
            competence.Status
        );
    }
}
