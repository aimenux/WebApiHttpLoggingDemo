using Microsoft.EntityFrameworkCore;

namespace Domain.Models;

public class QuoteDbContext : DbContext
{
    public QuoteDbContext(DbContextOptions<QuoteDbContext> options) : base(options)
    {
    }

    public DbSet<Quote> Quotes { get; set; }
}