using ResumoCash.Domain.Entities;
using ResumoCash.Domain.Repositories;

namespace ResumoCash.Application.Competences.Close;

public class CloseCompetenceService
{
    private readonly ICompetenceRepository _competenceRepository;

    public CloseCompetenceService(
        ICompetenceRepository competenceRepository)
    {
        _competenceRepository = competenceRepository;
    }

    public async Task ExecuteAsync(
        Guid userId,
        Guid competenceId,
        CancellationToken cancellationToken = default)
    {
        // Busca a competência do usuário
        var competence = await _competenceRepository.GetByIdAsync(
            userId,
            competenceId,
            cancellationToken
        );

        if (competence is null)
            throw new InvalidOperationException(
                "Competência não encontrada.");

        // Encerra a competência atual
        competence.Close();

        // Calcula o próximo mês
        var nextMonth = competence.Month.AddMonths(1);

        // Verifica se a próxima competência já existe
        var competences = await _competenceRepository.GetByUserIdAsync(
            userId,
            cancellationToken
        );

        if (competences.Any(x => x.Month == nextMonth))
            throw new InvalidOperationException(
                "A próxima competência já está cadastrada.");

        // Cria a próxima competência
        var nextCompetence = new Competence(
            userId,
            nextMonth
        );

        await _competenceRepository.AddAsync(
            nextCompetence,
            cancellationToken
        );

        // Salva o fechamento e a criação da próxima competência
        await _competenceRepository.SaveChangesAsync(
            cancellationToken
        );
    }
}