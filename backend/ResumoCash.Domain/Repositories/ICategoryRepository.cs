using ResumoCash.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResumoCash.Domain.Repositories;

public interface ICategoryRepository
{
    Task<Category?> GetByIdAsync(Guid userId, Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Category>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task AddAsync(Category category, CancellationToken cancellationToken = default);
    Task<bool> ExistsByNameAsync(Guid userId, string name, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
