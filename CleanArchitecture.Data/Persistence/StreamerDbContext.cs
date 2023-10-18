using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Infrastructure.Persistence
{
    public class StreamerDbContextSeed 
    {
        public static async Task SeedAsync(StreamerDbContext context, ILogger<StreamerDbContextSeed> logger) 
        {
            if (!context.Streamers!.Any())
            {
               await  context.Streamers!.AddRangeAsync(GetPreconfiguredStreamer());            

                await context.SaveChangesAsync();

                logger.LogInformation("insertando el seed al dbcontext", typeof(StreamerDbContext).Name);
            }
            
        
        }

        private static IEnumerable<Streamer> GetPreconfiguredStreamer() 
            => new List<Streamer>
            {
                new Streamer{ CreatedBy = "kevin", Nombre = "Monokai", Url = "http://monokai.com" },
                new Streamer{ CreatedBy = "kevin", Nombre = "visualKai", Url = "http://visualKai.com" },
            };
         
    
    
    }
    public class StreamerDbContext : DbContext
    {
        /*   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
           {
               optionsBuilder
                   .UseSqlServer(@"Server=DESKTOP-3VP26G1\CMARGOKCLU1; Database=StreamerDB; Persist Security Info=True; Trusted_Connection=True; MultipleActiveResultSets=True; TrustServerCertificate=true;")
               .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
               .EnableSensitiveDataLogging();
           }
        */

        public StreamerDbContext(DbContextOptions<StreamerDbContext> options) : base(options) 
        {
            
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseDomainModel>())
            {
                switch(entry.State) 
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = "system";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.UtcNow;
                        entry.Entity.LastModifiedBy = "system";
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Streamer>()
                .HasMany(m => m.Videos)
                .WithOne(m => m.Streamer)
                .HasForeignKey(m => m.StreamerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<Video>()
                .HasMany(p => p.Actores)
                .WithMany(t => t.Videos)
                .UsingEntity<VideoActor>(
                    pt => pt.HasKey(e => new { e.ActorId, e.VideoId })
                );


        }


        public DbSet<Streamer>? Streamers { get; set; }

        public DbSet<Video>? Videos { get; set; }

        public DbSet<Actor>? Actores { get; set; }

        public DbSet<Director>? Directores { get; set; }

    }
}
