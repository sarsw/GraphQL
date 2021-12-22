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
                .AddType<PlatformType>()
                .AddType<GraphQL.Commands.CommandType>()
                .AddFiltering()
                .AddSorting();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)      // the service container provides IWebHostEnvironment, IHostEnvironment, IConfiguration DI
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
