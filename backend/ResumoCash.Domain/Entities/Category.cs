using ResumoCash.Domain.Enums;
namespace ResumoCash.Domain.Entities;

public class Category
{
    public Category(
        Guid userId,
        string name,
        CategoryType type)
    {
        Id = Guid.NewGuid();
        UserId = ValidateUserId(userId);
        Name = ValidateName(name);
        Type = ValidateType(type);
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

    public void Update(
        string name,
        CategoryType type,
        CategoryStatus status)
    {
        Name = ValidateName(name);
        Type = ValidateType(type);
        Status = ValidateStatus(status);
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        Status = CategoryStatus.Inactive;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Activate()
    {
        Status = CategoryStatus.Active;
        UpdatedAt = DateTime.UtcNow;
    }

    private static Guid ValidateUserId(Guid userId)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("O usuário é obrigatório.");

        return userId;
    }

    private static string ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("O nome é obrigatório.");

        return name.Trim();
    }

    private static CategoryType ValidateType(CategoryType type)
    {
        if (!Enum.IsDefined(typeof(CategoryType), type))
            throw new ArgumentException("O tipo de categoria é inválido.");

        return type;
    }

    private static CategoryStatus ValidateStatus(CategoryStatus status)
    {
        if (!Enum.IsDefined(typeof(CategoryStatus), status))
            throw new ArgumentException("O status da categoria é inválido.");

        return status;
    }

}
