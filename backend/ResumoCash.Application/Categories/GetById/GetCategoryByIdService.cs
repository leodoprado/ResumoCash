using OneOf;
using ResumoCash.Domain.Common;
using ResumoCash.Domain.Common.Errors;
using ResumoCash.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResumoCash.Application.Categories.GetById;

public class GetCategoryByIdService
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoryByIdService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<OneOf<GetCategoryByIdResponse, Error>> ExecuteAsync(
        Guid userId,
        Guid categoryId,
        CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(userId, categoryId, cancellationToken);

        if (category is null)
          return CategoryErrors.NotFound;

        return new GetCategoryByIdResponse(
            category.Id,
            category.Name,
            category.Type,
            category.Status,
            category.CreatedAt
        );
    }
}
