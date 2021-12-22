using System.Linq;
using CommanderGQL.Data;
using CommanderGQL.Models;
using HotChocolate;
using HotChocolate.Types;

namespace CommanderGQL.GraphQL.Platforms
{
    public class PlatformType : ObjectType<Platform>    // using this code-first approach we can remove the [] annotations from the main models to clean them up
    {
        protected override void Configure(IObjectTypeDescriptor<Platform> descriptor)   // our Platform model to be presented
        {
            descriptor.Description("Represents any software or service that has commands");
            descriptor.Field(p => p.LicenseKey).Ignore();   // hide from the external view
            descriptor
                .Field(p => p.Commands)
                .ResolveWith<Resolvers>(p => p.GetCommands(default!, default!))
                .UseDbContext<AppDbContext>()
                .Description("List of commands for platform");
        }

        private class Resolvers     // this heavy lifting is done by the UseProjection in annotation code-first
        {
            
            public IQueryable<Command> GetCommands(Platform platform, [ScopedService] AppDbContext context)
            {
                return context.Commands.Where(p => p.PlatformId == platform.Id);
            }
        }
        
    }
}
