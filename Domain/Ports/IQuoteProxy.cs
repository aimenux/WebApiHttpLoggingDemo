using Domain.Models;

namespace Domain.Ports;

public interface IQuoteProxy
{
    Task<Quote> GetQuoteAsync(CancellationToken cancellationToken = default);
}