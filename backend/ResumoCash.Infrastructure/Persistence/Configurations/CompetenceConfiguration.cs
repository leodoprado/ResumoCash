using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumoCash.Domain.Entities;

namespace ResumoCash.Infrastructure.Persistence.Configurations;

public class CompetenceConfiguration
    : IEntityTypeConfiguration<Competence>
{
    public void Configure(EntityTypeBuilder<Competence> builder)
    {
        builder.ToTable("competences");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(x => x.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        builder.Property(x => x.Month)
            .HasColumnName("month")
            .IsRequired();

        builder.Property(x => x.Status)
            .HasColumnName("status")
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.ClosedAt)
            .HasColumnName("closed_at");

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .HasColumnName("updated_at");

        // Competence pertence a um User
        builder.HasOne(x => x.User)
            .WithMany(x => x.Competences)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Necessário para permitir FK composta
        // Transaction(UserId, CompetenceId)
        builder.HasAlternateKey(x => new
        {
            x.UserId,
            x.Id
        });

        // Um usuário não pode possuir duas
        // competências para o mesmo mês.
        builder.HasIndex(x => new
        {
            x.UserId,
            x.Month
        })
        .IsUnique();
    }
}