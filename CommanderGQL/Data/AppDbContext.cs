using Microsoft.EntityFrameworkCore;
using CommanderGQL.Models;

namespace CommanderGQL.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) :base(options)
        {
            
        }

        public DbSet<Platform>  Platforms { get; set; }       // table in the db
        public DbSet<Command>  Commands { get; set; }       // table in the db

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Platform>().HasMany(p => p.Commands) /*from platform perspective*/
                .WithOne(p => p.Platform!)/*ref to self and nullable*/
                .HasForeignKey(p => p.PlatformId);  //pltId is the link back to platforms
        }
    }
}
