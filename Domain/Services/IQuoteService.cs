using Domain.Models;

namespace Domain.Services;

public interface IQuoteService
{
    Task<Quote> AddQuoteAsync(CancellationToken cancellationToken = default);

    Task<Quote> AddQuoteAsync(Quote quote, CancellationToken cancellationToken = default);

    Task<ICollection<Quote>> GetQuotesAsync(CancellationToken cancellationToken = default);
}