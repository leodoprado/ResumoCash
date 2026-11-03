using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
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

    public async Task<CreateCategoryResponse> ExecuteAsync (
        Guid userId,
        CreateCategoryRequest request)
    {
        var category = new Category(userId,
                                    request.Name,
                                    request.Type);

        var existNameCategory = await _categoryRepository.ExistsByNameAsync(userId, request.Name);

        if (existNameCategory)
            throw new InvalidOperationException("Já existe uma categoria com esse nome.");

        await _categoryRepository.AddAsync(category);
        await _categoryRepository.SaveChangesAsync();

        return new CreateCategoryResponse(
            category.Id,
            category.Name
         );
    }
}
