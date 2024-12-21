using Bogus;
using GraphQL.Server.Domain;

namespace GraphQL.Server.Infrastructure.Persistence;

public static class SeedData
{
    public static void PersistData(IServiceProvider svp)
    {
        var scope = svp.CreateScope();
        var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var instructors = new Faker<Instructor>()
            .RuleFor(i => i.Name, f => f.Name.FullName())
            .RuleFor(i => i.Email, f => f.Internet.Email())
            .RuleFor(i => i.PhoneNumber, f => f.Phone.PhoneNumber())
            .Generate(1_000);
        
        ctx.Instructors.AddRange(instructors);
        
        var students = new Faker<Student>()
            .RuleFor(s => s.Name, f => f.Name.FullName())
            .RuleFor(s => s.Email, f => f.Internet.Email())
            .RuleFor(s => s.PhoneNumber, f => f.Phone.PhoneNumber())
            .RuleFor(s=>s.Gpa, f=>f.Random.Int(1, 4))
            .Generate(1_000);
        
        ctx.Students.AddRange(students);
        
        
        var courses = new Faker<Course>()
            .RuleFor(c => c.Title, f => f.Lorem.Sentence())
            .RuleFor(c => c.Description, f => f.Lorem.Paragraph())
            .RuleFor(c => c.Type, f => f.PickRandom<CourseType>())
            .RuleFor(c=>c.InstructorId, f=>f.PickRandom(instructors).Id)
            .Generate(1_000);
        
        ctx.Courses.AddRange(courses);
        
        
        ctx.SaveChanges();
    }
}