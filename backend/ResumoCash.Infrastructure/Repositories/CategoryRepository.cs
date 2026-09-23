using Microsoft.EntityFrameworkCore;
using ResumoCash.Domain.Entities;
using ResumoCash.Domain.Enums;
using ResumoCash.Domain.Repositories;
using ResumoCash.Infrastructure.Persistence;

namespace ResumoCash.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        Category category,
        CancellationToken cancellationToken = default)
    {
        await _context.Categories.AddAsync(
            category,
            cancellationToken);
    }

    public async Task<bool> ExistsByNameAsync(
        Guid userId,
        string name,
        CancellationToken cancellationToken = default)
    {
        return await _context.Categories
            .AnyAsync(
                x => x.UserId == userId &&
                x.Name == name,
                cancellationToken
             );
    }

    public async Task<Category?> GetByIdAsync(
        Guid userId,
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Categories
            .FirstOrDefaultAsync(
                x => x.Id == id && 
                x.UserId == userId &&
                x.Status == CategoryStatus.Active,
                cancellationToken);
    }

    public async Task<IEnumerable<Category>> GetByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Categories
            .AsNoTracking()
            .Where(
                x => x.UserId == userId &&
                x.Status == CategoryStatus.Active)
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}