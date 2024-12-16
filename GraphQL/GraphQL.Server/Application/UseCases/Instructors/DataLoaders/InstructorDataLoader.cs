using GraphQL.Server.Domain;
using GraphQL.Server.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Server.Application.UseCases.Instructors.DataLoaders;

public sealed class InstructorDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    [Service] AppDbContext dbContext)
    : BatchDataLoader<Guid, InstructorDto>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<Guid, InstructorDto>> LoadBatchAsync(IReadOnlyList<Guid> keys,
        CancellationToken cancellationToken)
    {
        Console.WriteLine($"Batch loading instructor count: {keys.Count}");
        var instructors = await dbContext.Instructors
            .Where(i => EF.Parameter(keys).Contains(i.Id))
            .Select(i => new InstructorDto(i.Id, i.Name, i.Email, i.PhoneNumber))
            .ToDictionaryAsync(i => i.Id, cancellationToken);

        return instructors;
    }
}