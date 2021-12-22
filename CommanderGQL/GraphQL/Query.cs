using System.Linq;
using CommanderGQL.Data;
using CommanderGQL.Models;
using HotChocolate;
using HotChocolate.Data;

namespace CommanderGQL.GraphQL
{
    public class Query
    {
        [UseDbContext(typeof(AppDbContext))]// gets a DBContext from the pool and returns the context to the pool after the query completes
        [UseFiltering]
        [UseSorting]
        public IQueryable<Platform> GetPlatform([ScopedService] AppDbContext context)     // using Service from the HotChoc framework
        {
            return context.Platforms;       // remember a scopedservice is created once per client request
        }

        [UseDbContext(typeof(AppDbContext))]// gets a DBContext from the pool and returns the context to the pool after the query completes
        [UseFiltering]
        [UseSorting]
        public IQueryable<Command> GetCommand([ScopedService] AppDbContext context)
        {
            return context.Commands;
        }
    }
}
