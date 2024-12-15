using GraphQL.Server.Domain;
using GraphQL.Server.Infrastructure.Persistence;

namespace GraphQL.Server.Application.UseCases.Instructors;

[QueryType]
public sealed class InstructorQuery
{
    public IQueryable<Instructor> GetInstructors([Service] AppDbContext ctx)
    {
        return ctx.Instructors;
    }
}