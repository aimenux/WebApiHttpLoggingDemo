using Api.Filters;

namespace Api;

public partial class Startup
{
    private static void AddMvc(WebApplicationBuilder builder)
    {
        builder.Services.AddControllers(options =>
        {
            options.Filters.Add(typeof(ApiExceptionFilter));
        });
    }
}