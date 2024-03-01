namespace Domain.Models;

public class Quote
{
    public string Id { get; init; }

    public string Author { get; init; }

    public string Description { get; init; }

    public static bool IsValid(Quote quote)
    {
        return quote != null
               && !string.IsNullOrWhiteSpace(quote.Author)
               && !string.IsNullOrWhiteSpace(quote.Description);
    }
}