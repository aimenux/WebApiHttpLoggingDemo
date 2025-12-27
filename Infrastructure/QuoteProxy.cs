using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Domain.Models;
using Domain.Ports;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Infrastructure;

public class QuoteProxy : IQuoteProxy
{
    private readonly HttpClient _client;
    private readonly IConfiguration _configuration;
    private readonly ILogger<IQuoteProxy> _logger;

    public QuoteProxy(HttpClient client, IConfiguration configuration, ILogger<IQuoteProxy> logger)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Quote> GetQuoteAsync(CancellationToken cancellationToken = default)
    {
        var relativePath = _configuration.GetValue<string>("QuoteApi:RelativePath");
        var quotes = await _client.GetFromJsonAsync<List<QuoteDto>>($"/{relativePath}", CancellationToken.None);
        var quote = quotes?.FirstOrDefault();
        if (quote is null) return null;
        return new Quote
        {
            Id = quote.Id ?? Guid.NewGuid().ToString(),
            Author = quote.Author,
            Description = quote.Content
        };
    }

    private sealed record QuoteDto
    {
        [JsonPropertyName("c")]
        public string Id { get; init; }

        [JsonPropertyName("a")]
        public string Author { get; init; }

        [JsonPropertyName("q")]
        public string Content { get; init; }
    }
}