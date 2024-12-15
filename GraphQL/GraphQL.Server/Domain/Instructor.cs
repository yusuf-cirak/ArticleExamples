namespace GraphQL.Server.Domain;

public sealed class Instructor : Entity
{
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
}

public sealed record InstructorInputType(string Name, string Email, string PhoneNumber);
public sealed record InstructorDto(Guid Id, string Name, string Email, string PhoneNumber);