namespace Domain.Models;

public class Quote
{
    public string Id { get; set; }

    public string Author { get; set; }

    public string Description { get; set; }

    public static bool IsValid(Quote quote)
    {
        return quote != null
               && !string.IsNullOrWhiteSpace(quote.Author)
               && !string.IsNullOrWhiteSpace(quote.Description);
    }
}