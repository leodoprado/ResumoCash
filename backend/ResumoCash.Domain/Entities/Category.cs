using ResumoCash.Domain.Enums;
namespace ResumoCash.Domain.Entities;

public class Category
{
    public Category(Guid userId, string name, CategoryType type)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("O usuário é obrigatório.");

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("O nome é obrigatório.");

        Id = Guid.NewGuid();
        UserId = userId;
        Name = name;
        Type = type;
        Status = CategoryStatus.Active;
        CreatedAt = DateTime.UtcNow;
    }

    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public CategoryType Type { get; private set; }
    public string Name { get; private set; }
    public CategoryStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public User User { get; private set; } = null!;
    public ICollection<Transaction> Transactions { get; private set; } = new List<Transaction>();

    public void Atualizar(string name, CategoryType type, CategoryStatus status)
    {
        Name = name;
        Type = type;
        Status = status;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Inativar()
    {
        Status = CategoryStatus.Inactive;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Ativar()
    {
        Status = CategoryStatus.Active;
        UpdatedAt = DateTime.UtcNow;
    }
}
