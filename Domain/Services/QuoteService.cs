using Domain.Exceptions;
using Domain.Models;
using Domain.Ports;

namespace Domain.Services;

public class QuoteService : IQuoteService
{
    private readonly IQuoteProxy _proxy;
    private readonly IQuoteRepository _repository;

    public QuoteService(IQuoteProxy proxy, IQuoteRepository quoteRepository)
    {
        _proxy = proxy ?? throw new ArgumentNullException(nameof(proxy));
        _repository = quoteRepository ?? throw new ArgumentNullException(nameof(quoteRepository));
    }

    public async Task<Quote> AddQuoteAsync(CancellationToken cancellationToken)
    {
        var quote = await _proxy.GetQuoteAsync(cancellationToken);
        await AddQuoteAsync(quote, cancellationToken);
        return quote;
    }

    public async Task<Quote> AddQuoteAsync(Quote quote, CancellationToken cancellationToken)
    {
        if (!Quote.IsValid(quote))
        {
            throw BusinessValidationException.QuoteIsNotValid();
        }

        await _repository.AddQuoteAsync(quote, cancellationToken);
        return quote;
    }

    public async Task<ICollection<Quote>> GetQuotesAsync(CancellationToken cancellationToken)
    {
        return await _repository.GetQuotesAsync(cancellationToken);
    }
}