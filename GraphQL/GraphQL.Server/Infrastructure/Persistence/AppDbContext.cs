using GraphQL.Server.Domain;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Server.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Course> Courses { get; set; }
    public DbSet<Instructor> Instructors { get; set; }
    public DbSet<Student> Students { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}