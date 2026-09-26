using Microsoft.AspNetCore.Mvc;
using ResumoCash.Application.Categories.Update;
using ResumoCash.Application.Transactions.Completed;
using ResumoCash.Application.Transactions.Create;
using ResumoCash.Application.Transactions.Delete;
using ResumoCash.Application.Transactions.GetAll;
using ResumoCash.Application.Transactions.GetById;
using ResumoCash.Application.Transactions.Reopen;
using ResumoCash.Application.Transactions.Update;

namespace ResumoCash.Api.Controllers;

[ApiController]
[Route("api/transactions")]
public class TransactionsController : ControllerBase
{
    private readonly CreateTransactionService _createTransactionService;
    private readonly GetTransactionsService _getAllTransactionsService;
    private readonly GetTransactionByIdService _getTransactionByIdService;
    private readonly UpdateTransactionService _updateTransactionService;
    private readonly DeleteTransactionService _deleteTransactionService;
    private readonly CompletedTransactionService _completeTransactionService;
    private readonly ReopenTransactionService _reopenTransactionService;

    public TransactionsController(
        CreateTransactionService createTransactionService,
        GetTransactionsService getAllTransactionsService,
        GetTransactionByIdService getTransactionByIdService,
        UpdateTransactionService updateTransactionService,
        DeleteTransactionService deleteTransactionService,
        CompletedTransactionService completeTransactionService,
        ReopenTransactionService reopenTransactionService)
    {
        _createTransactionService = createTransactionService;
        _getAllTransactionsService = getAllTransactionsService;
        _getTransactionByIdService = getTransactionByIdService;
        _updateTransactionService = updateTransactionService;
        _deleteTransactionService = deleteTransactionService;
        _completeTransactionService = completeTransactionService;
        _reopenTransactionService = reopenTransactionService;
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

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var userId = Guid.Parse(
            "11111111-1111-1111-1111-111111111111"
        );

        var result = await _getTransactionByIdService.ExecuteAsync(userId, id, cancellationToken);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        UpdateTransactionRequest request)
    {
        var userId = Guid.Parse(
            "11111111-1111-1111-1111-111111111111"
        );

        var result = await _updateTransactionService.ExecuteAsync(userId, id, request);

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

    [HttpPatch("{id:guid}/completed")]
    public async Task<IActionResult> Completed(
    Guid id,
    CancellationToken cancellationToken = default)
    {
        var userId = Guid.Parse(
            "11111111-1111-1111-1111-111111111111"
        );

        await _completeTransactionService.ExecuteAsync(
            userId,
            id,
            cancellationToken
        );

        return NoContent();
    }

    [HttpPatch("{id:guid}/reopen")]
    public async Task<IActionResult> Reopen(
    Guid id,
    CancellationToken cancellationToken = default)
    {
        var userId = Guid.Parse(
            "11111111-1111-1111-1111-111111111111"
        );

        await _reopenTransactionService.ExecuteAsync(
            userId,
            id,
            cancellationToken
        );

        return NoContent();
    }
}