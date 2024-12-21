using System.Text;
using GraphQL.Server.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextPool<AppDbContext>(opt => opt.UseInMemoryDatabase("db"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.Audience = "api://graphql-api";
    opt.Authority = "https://localhost:5001";
    opt.RequireHttpsMetadata = false;
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = "api://graphql-api",
        ValidIssuer = "https://localhost:5001",
        IssuerSigningKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Enumerable.Range(0, 32).Select(i => (char)i).ToArray())),

        ClockSkew = TimeSpan.Zero,
    };
});

builder.AddGraphQL()
    .AddTypes()
    .AddFiltering()
    .AddSorting()
    .AddProjections()
    .AddInMemorySubscriptions()
    .AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseWebSockets();
app.MapGraphQL();

SeedData.PersistData(app.Services);

app.RunWithGraphQLCommands(args);