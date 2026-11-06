using OneOf;
using ResumoCash.Domain.Common;
using ResumoCash.Domain.Common.Errors;
using ResumoCash.Domain.Entities;
using ResumoCash.Domain.Repositories;

namespace ResumoCash.Application.Categories.Create;

public class CreateCategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<OneOf<CreateCategoryResponse, AppError>> ExecuteAsync(
        Guid userId,
        CreateCategoryRequest request,
        CancellationToken cancellationToken)
    {
        var category = new Category(userId,
                                    request.Name,
                                    request.Type);

        var existNameCategory = await _categoryRepository.ExistsByNameAsync(
            userId,
            request.Name,
            cancellationToken
        );

        if (existNameCategory)
            return CategoryErrors.AlreadyExists;

        await _categoryRepository.AddAsync(
            category,
            cancellationToken
        );

        await _categoryRepository.SaveChangesAsync(cancellationToken);

        return new CreateCategoryResponse(
            category.Id,
            category.Name
         );
    }
}
