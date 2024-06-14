using Microsoft.AspNetCore.SpaServices.AngularCli;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// PRODUCTION ONLY
if (builder.Environment.IsProduction())
{
    builder.Services.AddSpaStaticFiles(configuration =>
    {
        // PRODUCTION
        configuration.RootPath = Path.Combine("..", "..", "frontend", "dist", "frontend"); //
    });
}
// You do not need to serve static Angular
// in development or the SPA root path!

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(option =>
    {
        option.RouteTemplate = "_api/{documentName}/swagger.json";
    });
    app.UseSwaggerUI(option =>
    {
        option.SwaggerEndpoint("/_api/v1/swagger.json", "AspNetAngularSamePort API");
        option.RoutePrefix = "_api/swagger";
    });
}

// PRODUCTION ONLY
if (builder.Environment.IsProduction())
{
    // You do not need access to static Angular files
    // in Development as you are only triggering
    // the "ng start" call below which runs Angular
    // builds in-memory.
    app.UseSpaStaticFiles();
}


app.UseHttpsRedirection();

// Be sure to call the Routing and Endpoint Middleware!
// This allows your WebApi to route to endpoints for
// data Angular calls inside the same project, but avoid
// ASP.NET setting a default homepage in the WebAPI.
// This is the key to letting the Single Page Application
// be the default route in the pipeline.

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/_api/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();


// At the end of the pipeline you always
// need some Single Page Application mapping
// to occur so in the browser your Angular app
// pops up in both environments.
// That is what "UseSpa" does. In development
// however it also calls the Angular CLI via the
// "ng start" command inside "packages.json"
// in your Angular folder.
app.UseSpa(spa =>
{
    if (builder.Environment.IsDevelopment())
    {
        // DEVELOPMENT
        // This gets the web root, and adds your path to your Angular folder.
        spa.Options.SourcePath = Path.Combine("..", "..", "frontend");

        // This calls the Angular CLI to build your Angular
        // project and call it via its server. This will
        // Load the Angular app into the browser, too,
        // And call your WebAPI for data.
        spa.UseAngularCliServer(npmScript: "start");

    }
    else
    {
        // PRODUCTION
        // DO NOT CALL THE Angular CLI and dev server in-memory
        // when in Production as will call only the static files!
    }
});

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
