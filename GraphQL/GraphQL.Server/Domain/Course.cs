
using GraphQL.Server.Application.UseCases.Instructors.DataLoaders;

namespace GraphQL.Server.Domain;

public sealed class Course : Entity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Instructor Instructor { get; set; }

    public List<Student> Students { get; set; } = [];

    public Guid InstructorId { get; set; }
    public CourseType Type { get; init; } = CourseType.Math;
}

public enum CourseType
{
    Math = 1,
    [GraphQLName("SCIENCE")]
    Science,
    [GraphQLName("HISTORY")]
    History,
    Art
}

public sealed class CourseDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    [IsProjected(true)] public Guid InstructorId { get; set; }

    public CourseType Type { get; set; }

    public Task<InstructorDto?> GetInstructor([Service] InstructorDataLoader dataLoader,
        CancellationToken cancellationToken)
    {
        return dataLoader.LoadAsync(InstructorId, cancellationToken);
    }
}

public sealed record CourseInputType(string Title, string Description, Guid InstructorId);