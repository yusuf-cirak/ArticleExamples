using GraphQL.Server.Infrastructure.Persistence;

namespace GraphQL.Server.Application.UseCases.Searchs;

[QueryType]
public static class SearchQuery
{
    public static IEnumerable<ISearchResultType> SearchResult([Service] AppDbContext ctx, string term)
    {
        var courses = ctx.Courses
            .Where(c => c.Title.Contains(term))
            .Select(c => new CourseSearchResultType(c.Id, c.Title))
            .AsEnumerable();

        var instructors = ctx.Instructors
            .Where(i => i.Name.Contains(term))
            .Select(i => new InstructorSearchResultType(i.Id, i.Name))
            .AsEnumerable();

        return courses.Concat<ISearchResultType>(instructors);
    }
    
    public static IEnumerable<ISearchUnionType> UnionResult([Service] AppDbContext ctx, string term)
    {
        var courses = ctx.Courses
            .Where(c => c.Title.Contains(term))
            .Select(c => new CourseSearchResultType(c.Id, c.Title))
            .AsEnumerable();

        var instructors = ctx.Instructors
            .Where(i => i.Name.Contains(term))
            .Select(i => new InstructorSearchResultType(i.Id, i.Name))
            .AsEnumerable();

        return courses.Concat<ISearchUnionType>(instructors);
    }
}