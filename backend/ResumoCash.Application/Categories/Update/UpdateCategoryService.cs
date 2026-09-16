using ResumoCash.Domain.Repositories;

namespace ResumoCash.Application.Categories.Update;

public class UpdateCategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public UpdateCategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<UpdateCategoryResponse> ExecuteAsync(
        Guid userId,
        Guid categoryId,
        UpdateCategoryRequest request,
        CancellationToken cancellationToken = default)
    {
        var category = await _categoryRepository.GetByIdAsync(
            categoryId,
            cancellationToken
        );

        if (category is null || category.UserId != userId)
        {
            throw new InvalidOperationException(
                "Categoria não encontrada."
            );
        }

        var name = request.Name.Trim();

        category.Atualizar(
            name,
            request.Type,
            request.Status
        );

        await _categoryRepository.SaveChangesAsync(cancellationToken);

        return new UpdateCategoryResponse(
            category.Id,
            category.Name,
            category.Type,
            category.Status
        );
    }
}