using Microsoft.EntityFrameworkCore;
using ResumoCash.Domain.Entities;
using ResumoCash.Domain.Enums;
using ResumoCash.Domain.Repositories;
using ResumoCash.Infrastructure.Persistence;

namespace ResumoCash.Infrastructure.Repositories;

public class CompetenceRepository : ICompetenceRepository
{
    private readonly AppDbContext _context;

    public CompetenceRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        Competence competence,
        CancellationToken cancellationToken = default)
    {
        await _context.Competences.AddAsync(
            competence,
            cancellationToken);
    }

    public async Task<Competence?> GetByIdAsync(
        Guid userId,
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Competences
            .FirstOrDefaultAsync(
                x =>
                    x.Id == id &&
                    x.UserId == userId,
                cancellationToken);
    }

    public async Task<Competence?> GetOpenAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Competences
            .FirstOrDefaultAsync(
                x =>
                    x.UserId == userId &&
                    x.Status == CompetenceStatus.Open,
                cancellationToken);
    }

    public async Task<IEnumerable<Competence>> GetByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Competences
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.Month)
            .ToListAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}