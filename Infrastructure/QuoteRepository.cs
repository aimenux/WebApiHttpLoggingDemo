using Domain.Models;
using Domain.Ports;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class QuoteRepository : IQuoteRepository
{
    private readonly QuoteDbContext _context;

    public QuoteRepository(QuoteDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task AddQuoteAsync(Quote quote, CancellationToken cancellationToken)
    {
        await _context.AddAsync(quote, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<ICollection<Quote>> GetQuotesAsync(CancellationToken cancellationToken)
    {
        return await _context.Quotes.ToListAsync(cancellationToken);
    }
}