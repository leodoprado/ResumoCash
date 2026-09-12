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

    public async Task<GetCategoryByIdResponse?> ExecuteAsync(
        Guid userId,
        Guid categoryId)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId);

        if (category is null)
          return null;

        if (category.UserId != userId)
          return null;

        return new GetCategoryByIdResponse(
            category.Id,
            category.Name,
            category.Type,
            category.Status,
            category.CreatedAt
        );
    }
}
