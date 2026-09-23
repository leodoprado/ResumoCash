using Microsoft.AspNetCore.Mvc;
using ResumoCash.Application.Transactions.Create;
using ResumoCash.Application.Transactions.Delete;
using ResumoCash.Application.Transactions.GetAll;

namespace ResumoCash.Api.Controllers;

[ApiController]
[Route("api/transactions")]
public class TransactionsController : ControllerBase
{
    private readonly CreateTransactionService _createTransactionService;
    private readonly GetTransactionsService _getAllTransactionsService;
    private readonly DeleteTransactionService _deleteTransactionService;

    public TransactionsController(
        CreateTransactionService createTransactionService,
        GetTransactionsService getAllTransactionsService,
        DeleteTransactionService deleteTransactionService)
    {
        _createTransactionService = createTransactionService;
        _getAllTransactionsService = getAllTransactionsService;
        _deleteTransactionService = deleteTransactionService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateTransactionRequest request,
        CancellationToken cancellationToken = default)
    {
        var userId = Guid.Parse(
            "11111111-1111-1111-1111-111111111111"
        );

        var result = await _createTransactionService.ExecuteAsync(userId, request, cancellationToken);

        return Created($"/api/transactions/{result.Id}", result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        CancellationToken cancellationToken = default)
    {
        var userId = Guid.Parse(
            "11111111-1111-1111-1111-111111111111"
        );

        var result = await _getAllTransactionsService.ExecuteAsync(userId, cancellationToken);

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var userId = Guid.Parse(
            "11111111-1111-1111-1111-111111111111"
        );

        await _deleteTransactionService.ExecuteAsync(
            userId,
            id, 
            cancellationToken
        );

        return NoContent();
    }
}
