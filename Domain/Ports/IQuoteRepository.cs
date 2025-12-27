using Domain.Models;

namespace Domain.Ports;

public interface IQuoteRepository
{
    Task AddQuoteAsync(Quote quote, CancellationToken cancellationToken);

    Task<ICollection<Quote>> GetQuotesAsync(CancellationToken cancellationToken);
}