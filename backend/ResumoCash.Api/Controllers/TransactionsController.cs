using Microsoft.AspNetCore.Mvc;
using ResumoCash.Api.Common.Extensions;
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
    public async Task<IActionResult> Create([FromBody] CreateTransactionRequest request, CancellationToken cancellationToken = default)
    {
        var userId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var transactionResult = await _createTransactionService.ExecuteAsync(userId, request, cancellationToken);

        if (transactionResult.IsError())
            return transactionResult.GetErrorResult().ToActionResult(this);

        var transaction = transactionResult.GetSuccessResult();

        return Created($"/api/transactions/{transaction.Id}", transaction);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var userId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var transactionResult = await _getAllTransactionsService.ExecuteAsync(userId, cancellationToken);

        return Ok(transactionResult);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        var userId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var transactionResult = await _getTransactionByIdService.ExecuteAsync(userId, id, cancellationToken);

        if (transactionResult.IsError())
            return transactionResult.GetErrorResult().ToActionResult(this);

        var transaction = transactionResult.GetSuccessResult();

        return Ok(transaction);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateTransactionRequest request, CancellationToken cancellationToken = default)
    {
        var userId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var transactionResult = await _updateTransactionService.ExecuteAsync(userId, id, request);

        if (transactionResult.IsError())
            return transactionResult.GetErrorResult().ToActionResult(this);

        var transaction = transactionResult.GetSuccessResult();

        return Ok(transaction);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
    {
        var userId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var transactionResult = await _deleteTransactionService.ExecuteAsync(userId, id, cancellationToken);

        if (transactionResult.IsError())
            return transactionResult.GetErrorResult().ToActionResult(this);

        return NoContent();
    }

    [HttpPatch("{id:guid}/completed")]
    public async Task<IActionResult> Completed(Guid id, CancellationToken cancellationToken = default)
    {
        var userId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var transactionResult = await _completeTransactionService.ExecuteAsync(userId, id, cancellationToken);

        if (transactionResult.IsError())
            return transactionResult.GetErrorResult().ToActionResult(this);

        return NoContent();
    }

    [HttpPatch("{id:guid}/reopen")]
    public async Task<IActionResult> Reopen(Guid id, CancellationToken cancellationToken = default)
    {
        var userId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var transactionResult = await _reopenTransactionService.ExecuteAsync(userId, id, cancellationToken);

        if (transactionResult.IsError())
            return transactionResult.GetErrorResult().ToActionResult(this);

        return NoContent();
    }
}
