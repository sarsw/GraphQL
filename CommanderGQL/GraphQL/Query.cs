using System.Linq;
using CommanderGQL.Data;
using CommanderGQL.Models;
using HotChocolate;

namespace CommanderGQL.GraphQL
{
    public class Query
    {
        public IQueryable<Platform> GetPlatform([Service] AppDbContext context)     // using Service from the HotChoc framework
        {
            return context.Platforms;
        }
    }
}
