namespace GraphQL.Server.Domain;

public abstract class Entity
{
    public Guid Id { get; init; } = Guid.CreateVersion7();
}