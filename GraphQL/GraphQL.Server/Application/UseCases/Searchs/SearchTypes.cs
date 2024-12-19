namespace GraphQL.Server.Application.UseCases.Searchs;

[InterfaceType]
public interface ISearchResultType
{
    public Guid Id { get; init; }
}

[UnionType]
public interface ISearchUnionType
{
}

[ObjectType("CourseSearchResult")]
public sealed record CourseSearchResultType(Guid Id, string Title) : ISearchResultType , ISearchUnionType;

[ObjectType("InstructorSearchResult")]
public sealed record InstructorSearchResultType(Guid Id, string Name) : ISearchResultType, ISearchUnionType;