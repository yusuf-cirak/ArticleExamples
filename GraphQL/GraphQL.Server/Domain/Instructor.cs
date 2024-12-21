namespace GraphQL.Server.Domain;

public sealed class Instructor : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
}

public sealed record InstructorInputType(string Name, string Email, string PhoneNumber);
public sealed record InstructorDto(Guid Id, string Name, string Email, string PhoneNumber);