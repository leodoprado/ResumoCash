using ResumoCash.Domain.Repositories;

namespace ResumoCash.Application.Categories.Delete;

public class DeleteCategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public DeleteCategoryService(
        ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task ExecuteAsync(
        Guid userId,
        Guid categoryId,
        CancellationToken cancellationToken = default)
    {
        var category = await _categoryRepository.GetByIdAsync(
            userId,
            categoryId,
            cancellationToken
        );

        if (category is null || category.UserId != userId)
        {
            throw new InvalidOperationException(
                "Categoria não encontrada."
            );
        }

        category.Inativar();

        await _categoryRepository.SaveChangesAsync(
            cancellationToken
        );
    }
}