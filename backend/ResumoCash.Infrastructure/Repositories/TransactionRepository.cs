using Microsoft.EntityFrameworkCore;
using ResumoCash.Domain.Entities;
using ResumoCash.Domain.Enums;
using ResumoCash.Domain.Repositories;
using ResumoCash.Infrastructure.Persistence;

namespace ResumoCash.Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _context;

    public TransactionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Transaction transaction, CancellationToken cancellationToken = default)
    {
        await _context.Transactions.AddAsync(transaction, cancellationToken);
    }

    public async Task<Transaction?> GetByIdAsync(Guid userId, Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Transactions
            .FirstOrDefaultAsync(x => 
                x.Id == id &&
                x.UserId == userId &&
                x.Status == TransactionStatus.Active, 
                cancellationToken);
    }

    public async Task<IEnumerable<Transaction>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.Transactions
            .AsNoTracking()
            .Include(x => x.Category)
            .Where(x => 
                x.UserId == userId &&
                x.Status == TransactionStatus.Active)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    } 

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
