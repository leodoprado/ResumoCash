using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumoCash.Domain.Entities;

namespace ResumoCash.Infrastructure.Persistence.Configurations;

public class TransactionConfiguration
    : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("transactions");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(x => x.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        builder.Property(x => x.CategoryId)
            .HasColumnName("category_id")
            .IsRequired();

        builder.Property(x => x.Description)
            .HasColumnName("description")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Amount)
            .HasColumnName("amount")
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.CompetenceMonth)
            .HasColumnName("competence_month")
            .IsRequired();

        builder.Property(x => x.DueDate)
            .HasColumnName("due_date");

        builder.Property(x => x.IsCompleted)
            .HasColumnName("is_completed")
            .IsRequired();

        builder.Property(x => x.CompletedAt)
            .HasColumnName("completed_at");

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .HasColumnName("updated_at");

        // Transaction pertence diretamente ao User
        builder.HasOne(x => x.User)
            .WithMany(x => x.Transactions)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Transaction pertence a uma Category
        // e a Category deve pertencer ao mesmo User.
        builder.HasOne(x => x.Category)
            .WithMany(x => x.Transactions)
            .HasForeignKey(x => new
            {
                x.UserId,
                x.CategoryId
            })
            .HasPrincipalKey(x => new
            {
                x.UserId,
                x.Id
            })
            .OnDelete(DeleteBehavior.Restrict);

        // Muito útil para consultas mensais
        builder.HasIndex(x => new
        {
            x.UserId,
            x.CompetenceMonth
        });
    }
}