using CommanderGQL.Data;
using CommanderGQL.GraphQL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GraphQL.Server.Ui.Voyager;
using CommanderGQL.GraphQL.Platforms;
using CommanderGQL.GraphQL.Commands;
using System.Data;

namespace CommanderGQL
{
    public class Startup
    {
        private readonly IConfiguration _cfg;

        public Startup(IConfiguration cfg)
        {
            _cfg = cfg;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPooledDbContextFactory<AppDbContext>(opt => opt.UseSqlServer(_cfg.GetConnectionString("CommandConStr")));
            services
                .AddGraphQLServer() /* adds the server!*/
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddSubscriptionType<Subscription>()
                .AddType<PlatformType>()
                .AddType<GraphQL.Commands.CommandType>()
                .AddFiltering()
                .AddSorting()
                .AddInMemorySubscriptions();    // to track the subscribers in memory, a production deployment might use a persistence layer
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)      // the service container provides IWebHostEnvironment, IHostEnvironment, IConfiguration DI
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseWebSockets();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });

            app.UseGraphQLVoyager(new VoyagerOptions() {
                GraphQLEndPoint = "/graphql"},
                "/graphql-voyager");
        }
    }
}
