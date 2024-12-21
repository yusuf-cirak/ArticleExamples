using GraphQL.Server.Domain;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;

namespace GraphQL.Server.Application.UseCases.Courses;

[SubscriptionType]
public sealed class CourseSubscription
{
    [Subscribe]
    public Course OnCourseCreated([EventMessage] Course course) => course;

    [Subscribe(MessageType = typeof(CourseDto))]
    public ValueTask<ISourceStream<CourseDto>> OnCourseUpdatedAsync(string courseId,
        [Service] ITopicEventReceiver eventReceiver)
    {
        var topicName = $"{courseId}_{nameof(CourseSubscription.OnCourseUpdatedAsync)}";
        return eventReceiver.SubscribeAsync<CourseDto>(topicName);
    }
}