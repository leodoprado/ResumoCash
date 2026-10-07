using ResumoCash.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResumoCash.Domain.Repositories;

public interface ICompetenceRepository
{
    Task<Competence?> GetByIdAsync(Guid userId, Guid id, CancellationToken cancellationToken = default);
    Task<Competence?> GetOpenAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Competence>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task AddAsync(Competence competence, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
