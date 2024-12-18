using GraphQL.Server.Domain;
using HotChocolate.Data.Filters;

namespace GraphQL.Server.Application.UseCases.Courses.FilterTypes;

public sealed class CourseFilterType : FilterInputType<CourseDto>
{
    protected override void Configure(IFilterInputTypeDescriptor<CourseDto> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Ignore(c => c.InstructorId);
    }
}