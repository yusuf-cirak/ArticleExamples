using GraphQL.Server.Domain;
using HotChocolate.Data.Sorting;

namespace GraphQL.Server.Application.UseCases.Courses.Sorters;

public sealed class CourseSortType : SortInputType<CourseDto>
{
    protected override void Configure(ISortInputTypeDescriptor<CourseDto> descriptor)
    {
        descriptor.Ignore(c => c.Id);

        descriptor.Field(c => c.Description).Name("courseDescription");
        
        base.Configure(descriptor);
    }
}