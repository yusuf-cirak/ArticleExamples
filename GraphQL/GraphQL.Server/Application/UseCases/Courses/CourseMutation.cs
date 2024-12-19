using GraphQL.Server.Domain;
using GraphQL.Server.Infrastructure.Persistence;
using HotChocolate.Subscriptions;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Server.Application.UseCases.Courses;
[MutationType]
public sealed class CourseMutation
{
    public CourseDto CreateCourse(CourseInputType input, [Service] ITopicEventSender topicEventSender,
        [Service] AppDbContext ctx)
    {
        var course = new Course
        {
            Title = input.Title,
            Description = input.Description,
            InstructorId = input.InstructorId,
        };

        ctx.Courses.Add(course);
        ctx.SaveChanges();

        topicEventSender.SendAsync(nameof(CourseSubscription.OnCourseCreated), course);

        return new CourseDto()
        {
            Id = course.Id,
            Description = course.Description,
            Title = course.Title,
            InstructorId = input.InstructorId
        };
    }

    public CourseDto UpdateCourse(Guid id, CourseInputType input, [Service] ITopicEventSender topicEventSender,
        [Service] AppDbContext ctx)
    {
        var course = ctx.Courses
            .Include(course => course.Instructor)
            .Include(course => course.Students)
            .FirstOrDefault(c => c.Id == id);

        if (course is null)
        {
            throw new GraphQLException(new Error("Course not found", "COURSE_NOT_FOUND"));
        }

        course.Title = input.Title;
        course.Description = input.Description;

        ctx.Courses.Update(course);

        ctx.SaveChanges();

        var courseDto = new CourseDto()
        {
            Id = course.Id,
            Description = course.Description,
            Title = course.Title,
            InstructorId = input.InstructorId,
        };

        var topicName = $"{id}_{nameof(CourseSubscription.OnCourseUpdatedAsync)}";
        topicEventSender.SendAsync<CourseDto>(topicName, courseDto);

        return courseDto;
    }


    public bool DeleteCourse(Guid id, [Service] AppDbContext ctx)
    {
        var course = ctx.Courses.FirstOrDefault(c => c.Id == id);

        if (course is null)
        {
            throw new GraphQLException(new Error("Course not found", "COURSE_NOT_FOUND"));
        }

        ctx.Courses.Remove(course);
        ctx.SaveChanges();

        return true;
    }
}