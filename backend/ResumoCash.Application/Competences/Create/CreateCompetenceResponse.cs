using ResumoCash.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResumoCash.Application.Competences.Create;

public record CreateCompetenceResponse (
    Guid Id,
    DateOnly Month,
    CompetenceStatus Status
);
