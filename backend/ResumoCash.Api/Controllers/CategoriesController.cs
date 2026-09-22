using Microsoft.AspNetCore.Mvc;
using ResumoCash.Application.Categories.Create;
using ResumoCash.Application.Categories.Delete;
using ResumoCash.Application.Categories.GetAll;
using ResumoCash.Application.Categories.GetById;
using ResumoCash.Application.Categories.Update;

namespace ResumoCash.Api.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoriesController : ControllerBase
{
    private readonly CreateCategoryService _createCategoryService;
    private readonly GetCategoryByIdService _getCategoryByIdService;
    private readonly GetCategoriesService _getCategoriesService;
    private readonly UpdateCategoryService _updateCategoryService;
    private readonly DeleteCategoryService _deleteCategoryService;

    public CategoriesController(
        CreateCategoryService createCategoryService, 
        GetCategoryByIdService getCategoryByIdService,
        GetCategoriesService getCategoriesService,
        UpdateCategoryService updateCategoryService,
        DeleteCategoryService deleteCategoryService)
    {
        _createCategoryService = createCategoryService;
        _getCategoryByIdService = getCategoryByIdService;
        _getCategoriesService = getCategoriesService;
        _updateCategoryService = updateCategoryService;
        _deleteCategoryService = deleteCategoryService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request)
    {
        var userId = Guid.Parse(
            "11111111-1111-1111-1111-111111111111"
        );

        var result = await _createCategoryService.ExecuteAsync(userId, request);

        return Created($"/api/categories/{result.Id}", result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var userId = Guid.Parse(
            "11111111-1111-1111-1111-111111111111"
        );
        var categories = await _getCategoriesService.ExecuteAsync(userId);
        return Ok(categories);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var userId = Guid.Parse(
            "11111111-1111-1111-1111-111111111111"
        );

        var category = await _getCategoryByIdService
            .ExecuteAsync(userId, id);

        if (category is null)
            return NotFound();

        return Ok(category);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        UpdateCategoryRequest request)
    {
        var userId = Guid.Parse(
            "11111111-1111-1111-1111-111111111111"
        );

        var result = await _updateCategoryService.ExecuteAsync(userId, id, request);
            
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(
            "11111111-1111-1111-1111-111111111111"
        );

        await _deleteCategoryService.ExecuteAsync(
            userId,
            id,
            cancellationToken
        );

        return NoContent();
    }
}
