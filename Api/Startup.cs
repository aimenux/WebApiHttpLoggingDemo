namespace Api;

public partial class Startup
{
    public void ConfigureServices(WebApplicationBuilder builder)
    {
        AddMvc(builder);
        AddSwagger(builder);
        AddHealthChecks(builder);
        AddLogging(builder);
        AddApplicationInsightsTelemetry(builder);
        AddDependencies(builder);
    }

    public void Configure(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(options => options.DisplayRequestDuration());
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        UseHttpLogging(app);

        app.UseAuthorization();

        app.MapControllers();

        MapHealthChecks(app);
    }
}