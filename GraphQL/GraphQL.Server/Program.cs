using GraphQL.Server.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextPool<AppDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.AddGraphQL().AddTypes();

var app = builder.Build();

app.MapGraphQL();

SeedData.PersistData(app.Services);

app.RunWithGraphQLCommands(args);