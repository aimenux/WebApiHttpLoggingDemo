using Domain.Models;

namespace Domain.Services;

public interface IQuoteService
{
    Task<Quote> AddQuoteAsync(CancellationToken cancellationToken);

    Task<Quote> AddQuoteAsync(Quote quote, CancellationToken cancellationToken);

    Task<ICollection<Quote>> GetQuotesAsync(CancellationToken cancellationToken);
}