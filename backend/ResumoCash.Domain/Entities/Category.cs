using ResumoCash.Domain.Enums;
namespace ResumoCash.Domain.Entities;

public class Category
{
    public Category(Guid userId, string name, TransactionType type)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("O usuário é obrigatório.");

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("O nome é obrigatório.");

        Id = Guid.NewGuid();
        UserId = userId;
        Name = name;
        Type = type;
        Active = true;
        CreatedAt = DateTime.UtcNow;
    }

    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public TransactionType Type { get; private set; }
    public string Name { get; private set; }
    public bool Active { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public User User { get; private set; } = null!;
    public ICollection<Transaction> Transactions { get; private set; } = new List<Transaction>();

    public void AtualizarNome(string name)
    {
        Name = name;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Desativar()
    {
        Active = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Ativar()
    {
        Active = true;
        UpdatedAt = DateTime.UtcNow;
    }
}
