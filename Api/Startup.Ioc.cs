using Domain.Models;
using Domain.Ports;
using Domain.Services;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using static Api.Constants;

namespace Api;

public partial class Startup
{
    private static void AddDependencies(WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<QuoteDbContext>(options =>
        {
            options.UseInMemoryDatabase(DatabaseName);
        });

        builder.Services.AddHttpClient<IQuoteProxy, QuoteProxy>(client =>
        {
            var url = builder.Configuration.GetValue<string>("QuoteApi:BaseUrl");
            client.BaseAddress = new Uri(url);
        });

        builder.Services.AddTransient<IQuoteRepository, QuoteRepository>();
        builder.Services.AddTransient<IQuoteService, QuoteService>();
    }
}