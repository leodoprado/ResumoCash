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

    public async Task<CreateCompetenceResponse> ExecuteAsync(
        Guid userId,
        CreateCompetenceRequest request,
        CancellationToken cancellationToken = default)
    {
        // Verifica se existe uma competência aberta
        var openCompetence = await _competenceRepository
            .GetOpenAsync(userId, cancellationToken);

        if (openCompetence is not null)
            throw new InvalidOperationException(
                "O usuário já possui uma competência aberta.");

        // Normaliza o mês
        var month = new DateOnly(
            request.Month.Year,
            request.Month.Month,
            1);

        // Verifica se já existe uma competência nesse período
        var competences = await _competenceRepository
            .GetByUserIdAsync(userId, cancellationToken);

        if (competences.Any(x => x.Month == month))
            throw new InvalidOperationException(
                "Já existe uma competência cadastrada para este mês.");

        // Cria a entidade
        var competence = new Competence(userId, month);

        // Persiste
        await _competenceRepository.AddAsync(
            competence,
            cancellationToken);

        await _competenceRepository.SaveChangesAsync(
            cancellationToken);

        return new CreateCompetenceResponse(
            competence.Id,
            competence.Month,
            competence.Status);
    }
}