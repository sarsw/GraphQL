using System.Threading;
using System.Threading.Tasks;
using CommanderGQL.Data;
using CommanderGQL.GraphQL.Commands;
using CommanderGQL.GraphQL.Platforms;
using CommanderGQL.Models;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Subscriptions;

namespace CommanderGQL.GraphQL
{
    public class Mutation
    {
        [UseDbContext(typeof(AppDbContext))]
        public async Task<AddPlatformPayload> AddPlatformAsync(AddPlatformInput input,
            [ScopedService] AppDbContext context,
            [Service] ITopicEventSender eventSender,/*to send the event*/
            CancellationToken cancellationToken)/*handler for cancelling async events*/
        {
            var platform = new Platform { Name = input.Name };

            context.Platforms.Add(platform);

            await context.SaveChangesAsync(cancellationToken);

            await eventSender.SendAsync(nameof(Subscription.OnPlatformAdded), platform, cancellationToken);//noptify websocket audience

            return new AddPlatformPayload(platform);
        }     
        [UseDbContext(typeof(AppDbContext))]
        public async Task<AddCommandPayload> AddCommandAsync(AddCommandInput input,
            [ScopedService] AppDbContext context)
        {
            var cmd = new Command { HowTo = input.HowTo, CommandLine = input.CommandLine, PlatformId = input.PlatformId };

            context.Commands.Add(cmd);

            await context.SaveChangesAsync();

            return new AddCommandPayload(cmd);
        }     
    }
}
