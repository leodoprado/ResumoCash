using ResumoCash.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResumoCash.Domain.Repositories;

public interface ICategoryRepository
{
    Task<Category?> GetByIdAsync(Guid id);
    Task<IEnumerable<Category>> GetByUserIdAsync(Guid userId);
    Task AddAsync(Category category);
    Task<bool> ExistsByNameAsync(Guid userId, string name);
    Task SaveChangesAsync();
}
