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
            // from a platform
            modelBuilder.Entity<Platform>().HasMany(p => p.Commands) /*from platform perspective*/
                .WithOne(p => p.Platform!)/*ref to self and nullable*/
                .HasForeignKey(p => p.PlatformId);  //pltId is the link back to platforms
        
            // from a command
            modelBuilder.Entity<Command>().HasOne(p => p.Platform) /*a command belongs to one platform*/
                .WithMany(p => p.Commands)/*many commands, relationship to self*/
                .HasForeignKey(p => p.PlatformId);
        }
    }
}
