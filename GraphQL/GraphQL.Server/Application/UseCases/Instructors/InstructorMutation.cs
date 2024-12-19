using GraphQL.Server.Domain;
using GraphQL.Server.Infrastructure.Persistence;
using HotChocolate.AspNetCore;

namespace GraphQL.Server.Application.UseCases.Instructors;

[MutationType]
public sealed class InstructorMutation
{
    public InstructorDto CreateInstructor(InstructorInputType input, [Service] AppDbContext ctx)
    {
        var instructor = new Instructor
        {
            Name = input.Name,
            Email = input.Email,
            PhoneNumber = input.PhoneNumber,
        };

        ctx.Instructors.Add(instructor);
        ctx.SaveChanges();

        return new InstructorDto(instructor.Id, instructor.Name, instructor.Email, instructor.PhoneNumber);
    }
    
    public InstructorDto UpdateInstructor(Guid id,InstructorInputType input, [Service] AppDbContext ctx)
    {
        var instructor = ctx.Instructors.SingleOrDefault(i => i.Id == id);

        if (instructor is null)
        {
            throw new GraphQLException(new Error("Instructor not found", "NOT_FOUND"));
        }

        instructor.Email = input.Email;
        instructor.Name = input.Name;
        instructor.PhoneNumber = input.PhoneNumber;
        ctx.SaveChanges();

        return new InstructorDto(instructor.Id, instructor.Name, instructor.Email, instructor.PhoneNumber);
    }
    
    public bool DeleteInstructor(Guid id, [Service] AppDbContext ctx)
    {
        return ctx.Instructors.Remove(ctx.Instructors.Find(id)) is not null;
    }
    
    
}