using System.Threading.Tasks;
using CommanderGQL.Data;
using CommanderGQL.GraphQL.Commands;
using CommanderGQL.GraphQL.Platforms;
using CommanderGQL.Models;
using HotChocolate;
using HotChocolate.Data;

namespace CommanderGQL.GraphQL
{
    public class Mutation
    {
        [UseDbContext(typeof(AppDbContext))]
        public async Task<AddPlatformPayload> AddPlatformAsync(AddPlatformInput input,
            [ScopedService] AppDbContext context)
        {
            var platform = new Platform { Name = input.Name };

            context.Platforms.Add(platform);

            await context.SaveChangesAsync();

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
