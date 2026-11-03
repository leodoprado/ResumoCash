using ResumoCash.Domain.Enums;

namespace ResumoCash.Application.Competences.GetAll;

public record GetCompetenceResponse(
    Guid Id,
    DateOnly Month,
    CompetenceStatus Status
);