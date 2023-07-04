using Kwetter_Post_API.DAL.Models;
using MassTransit;

namespace Kwetter_Interaction_API.Core.Consumers;

public class PostConsumer : IConsumer<Post>
{
    public Task Consume(ConsumeContext<Post> context)
    {
        throw new NotImplementedException();
    }
}