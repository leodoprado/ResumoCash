using OneOf;
using ResumoCash.Domain.Common;
using ResumoCash.Domain.Common.Errors;
using ResumoCash.Domain.Repositories;
using Success = OneOf.Types.Success;

namespace ResumoCash.Application.Categories.Delete;

public class DeleteCategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public DeleteCategoryService(
        ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<OneOf<Success, Error>> ExecuteAsync(
        Guid userId,
        Guid categoryId,
        CancellationToken cancellationToken = default)
    {
        var category = await _categoryRepository.GetByIdAsync(
            userId,
            categoryId,
            cancellationToken
        );

        if (category is null)
            return CategoryErrors.NotFound;

        category.Deactivate();

        await _categoryRepository.SaveChangesAsync(
            cancellationToken
        );

        return new Success();
    }
}
