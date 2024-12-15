namespace GraphQL.Server.Domain;

public sealed class Student : Entity
{
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
    [GraphQLName("gpa")] public int Gpa { get; set; }
    public List<Course> Courses { get; init; } = new();
}