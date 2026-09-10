using Microsoft.AspNetCore.Mvc;
using ResumoCash.Application.Categories.Create;
using ResumoCash.Application.Categories.GetById;

namespace ResumoCash.Api.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoriesController : ControllerBase
{
    private readonly CreateCategoryService _createCategoryService;
    private readonly GetCategoryByIdService _getCategoryByIdService;

    public CategoriesController(CreateCategoryService createCategoryService, GetCategoryByIdService getCategoryByIdService)
    {
        _createCategoryService = createCategoryService;
        _getCategoryByIdService = getCategoryByIdService;

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
}
