using ResumoCash.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResumoCash.Application.Categories.GetAll;

public class GetCategoriesService
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoriesService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<GetCategoriesResponse>> ExecuteAsync(Guid userId)
    {
        var categories = await _categoryRepository.GetByUserIdAsync(userId);

        return categories.Select(category =>
            new GetCategoriesResponse(
                category.Id,
                category.Name,
                category.Type,
                category.Status
            )
        );
    }
}
