using ResumoCash.Domain.Entities;
using ResumoCash.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

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
        var name = request.Name.Trim();

        var exists = await _categoryRepository.ExistsByNameAsync(userId, request.Name);

        if (exists)
            throw new InvalidOperationException("Já existe uma categoria com esse nome.");

        var category = new Category(
            userId,
            name,
            request.Type);

        await _categoryRepository.AddAsync(category);
        await _categoryRepository.SaveChangesAsync();

        return new CreateCategoryResponse(
            category.Id,
            category.Name
         );
    }
}
