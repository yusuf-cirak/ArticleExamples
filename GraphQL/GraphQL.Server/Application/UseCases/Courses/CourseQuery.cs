using GraphQL.Server.Application.UseCases.Courses.FilterTypes;
using GraphQL.Server.Application.UseCases.Courses.Sorters;
using GraphQL.Server.Domain;
using GraphQL.Server.Infrastructure.Persistence;
using HotChocolate.Authorization;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Server.Application.UseCases.Courses;
[QueryType]
public sealed class CourseQuery
{
    // [Authorize]
    // [UsePaging(AllowBackwardPagination = true, IncludeTotalCount = true)]
    // [UseProjection]
    // [UseFiltering(typeof(CourseFilterType))]
    // [UseSorting(typeof(CourseSortType))]
    // public IQueryable<CourseDto> GetCourses([Service] AppDbContext ctx)
    // {
    //     return ctx.Courses
    //     .Select(c => new CourseDto()
    //     {
    //         Description = c.Description,
    //         Id = c.Id,
    //         Title = c.Title,
    //         InstructorId = c.InstructorId
    //     });
    // }
    
    [UseOffsetPaging(IncludeTotalCount = true)]
    public IEnumerable<CourseDto> GetOffsetCourses([Service] AppDbContext ctx)
    {
        return ctx.Courses
            .Select(c => new CourseDto()
            {
                Description = c.Description,
                Id = c.Id,
                Title = c.Title,
                InstructorId = c.InstructorId
            });
    }
    
    
    [UsePaging(IncludeTotalCount = true)]
    public IEnumerable<CourseDto> GetCursorCourses([Service] AppDbContext ctx)
    {
        return ctx.Courses
            .Select(c => new CourseDto()
            {
                Description = c.Description,
                Id = c.Id,
                Title = c.Title,
                InstructorId = c.InstructorId
            })
            .OrderBy(c => c.Id);
    }
    
    
    [UseFiltering(typeof(CourseFilterType))]
    [UseSorting(typeof(CourseSortType))]
    public IQueryable<CourseDto> GetCourses([Service] AppDbContext ctx)
    {
        return ctx.Courses
        .Select(c => new CourseDto()
        {
            Description = c.Description,
            Id = c.Id,
            Title = c.Title,
            InstructorId = c.InstructorId,
            Type = c.Type
        });
    }
}