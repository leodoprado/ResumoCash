using OneOf;
using ResumoCash.Domain.Common;
using ResumoCash.Domain.Common.Errors;
using ResumoCash.Domain.Repositories;

namespace ResumoCash.Application.Categories.Update;

public class UpdateCategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public UpdateCategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<OneOf<UpdateCategoryResponse, AppError>> ExecuteAsync(
        Guid userId,
        Guid categoryId,
        UpdateCategoryRequest request,
        CancellationToken cancellationToken = default)
    {
        var category = await _categoryRepository.GetByIdAsync(
            userId,
            categoryId,
            cancellationToken
        );

        if (category is null)
            return CategoryErrors.NotFound;

        var name = request.Name.Trim();

        category.Update(
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
