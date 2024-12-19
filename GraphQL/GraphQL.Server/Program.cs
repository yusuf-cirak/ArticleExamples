using GraphQL.Server.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextPool<AppDbContext>(opt => opt.UseInMemoryDatabase("db"));

builder.AddGraphQL()
    .AddTypes()
    .AddFiltering()
    .AddSorting()
    .AddProjections()
    .AddInMemorySubscriptions();

var app = builder.Build();

app.UseWebSockets();
app.MapGraphQL();

SeedData.PersistData(app.Services);

app.RunWithGraphQLCommands(args);