using WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.ConfigureLogging();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseLogging();

app.MapGet("/log", (ILogger<Program> logger) =>
    {
        const string requestName = "log";
        const string authLog = "auth";
        const string transaction = "transaction";
        try
        {
            logger.LogInformation("{Type} - {RequestName} has started", transaction, requestName);
            logger.LogInformation("{Type} - {RequestName} : Failure", authLog, requestName);
            return Results.Ok();
        }
        finally
        {
            logger.LogInformation("{Type} - {RequestName}: has finished", transaction, requestName);
        }
    })
    .WithName("Logging")
    .WithOpenApi();

app.Run();