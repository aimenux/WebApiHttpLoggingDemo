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
        var dto = await _client.GetFromJsonAsync<QuoteDto>($"/{relativePath}", CancellationToken.None);
        if (dto is null) return null;
        return new Quote
        {
            Id = dto.Id,
            Author = dto.Author,
            Description = dto.Content
        };
    }

    internal class QuoteDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("author")]
        public string Author { get; set; }

        [JsonPropertyName("en")]
        public string Content { get; set; }
    }
}