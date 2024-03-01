using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuotesController : ControllerBase
{
    private readonly IQuoteService _service;

    public QuotesController(IQuoteService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    [HttpPost("auto")]
    public async Task<Quote> AddQuoteAsync(CancellationToken cancellationToken)
    {
        var quotes = await _service.AddQuoteAsync(cancellationToken);
        return quotes;
    }

    [HttpPost("add")]
    public async Task<Quote> AddQuoteAsync(Quote quote, CancellationToken cancellationToken)
    {
        var quotes = await _service.AddQuoteAsync(quote, cancellationToken);
        return quotes;
    }

    [HttpGet("list")]
    public async Task<ICollection<Quote>> GetQuotesAsync(CancellationToken cancellationToken)
    {
        var quotes = await _service.GetQuotesAsync(cancellationToken);
        return quotes;
    }
}