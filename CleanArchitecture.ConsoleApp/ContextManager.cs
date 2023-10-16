using CleanArchitecture.Data;
using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.ConsoleApp
{
    public static class ContextManager
    {
        public static async Task AddNewRecords(StreamerDbContext dbContext)
        {

            Streamer streamer = new()
            {
                Nombre = "disney",
                Url = "https://www.disney.com"
            };

            dbContext!.Streamers!.Add(streamer);

            await dbContext.SaveChangesAsync();


            var movies = new List<Video>
            {
                new Video{
                   Nombre = "La Cenicienta",
                   StreamerId = streamer.Id
                },
                new Video{
                   Nombre = "1001 dalmatas",
                   StreamerId = streamer.Id
                },
                new Video{
                   Nombre = "El Jorobado de Notredame",
                   StreamerId = streamer.Id
                },
                new Video{
                   Nombre = "Star Wars",
                   StreamerId = streamer.Id
                },
            };


            await dbContext.AddRangeAsync(movies);
            await dbContext.SaveChangesAsync();

        }

        public static void QueryStreamingServices(StreamerDbContext dbContext)
        {
            var streamers = dbContext!.Streamers!.ToList();
            foreach (var streamer in streamers)
            {
                Console.WriteLine($"{streamer.Id} - {streamer.Nombre}");
            }

        }

        public static async Task QueryFilterComplete(StreamerDbContext dbContext)
        {
            Console.WriteLine($"Ingrese una compania de streaming:");
            var streamingNombre = Console.ReadLine();

            var streamers = await dbContext!.Streamers!
                .Where(x => x.Nombre.Equals(streamingNombre))
                .ToListAsync();

            Console.Out.WriteLine();
            foreach (var streamer in streamers)
            {
                Console.WriteLine($"{streamer.Id} -  {streamer.Nombre}");
            }        
           
        }

        public static async Task QueryFilterContains(StreamerDbContext dbContext)
        {
            Console.WriteLine($"Ingrese una compania de streaming:");
            var streamingNombre = Console.ReadLine();

            var streamerPartialResults = await dbContext!.Streamers!
                .Where(x => x.Nombre.Contains(streamingNombre)).
                ToListAsync();

            Console.Out.WriteLine();
            foreach (var streamer in streamerPartialResults)
            {
                Console.WriteLine($"{streamer.Id} -  {streamer.Nombre}");
            }

          //  var streamerPartialResultsV2 = await dbContext!.Streamers!
           //     .Where(x => EF.Functions.Like(x.Nombre, $"%{streamingNombre}%")).ToListAsync();

        }

        public static async Task QueryMethods(StreamerDbContext dbContext)
        {
            var streamer = dbContext!.Streamers!;

            var firstAsync = await streamer.Where(y => y.Nombre.Contains("a")).FirstAsync();

            var firstOrDefaultAsync = await streamer.Where(y => y.Nombre.Contains("a")).FirstOrDefaultAsync();

            var firstOrDefault_v2 = await streamer.FirstOrDefaultAsync(y => y.Nombre.Contains("a"));



            var singleAsync = await streamer.Where(y => y.Id == 1).SingleAsync();
            var singleOrDefaultAsync = await streamer.Where(y => y.Id == 1).SingleOrDefaultAsync();


            var resultado = await streamer.FindAsync(1);

            var count = await streamer.CountAsync();
            var longAccount = await streamer.LongCountAsync();
            var min = await streamer.MinAsync();
            var max = await streamer.MaxAsync();

        }

        public static async Task QueryLinq(StreamerDbContext dbContext)
        {
            Console.WriteLine($"Ingrese el servicio de streaming");
            var streamerNombre = Console.ReadLine();

            var streamers = await (from i in dbContext.Streamers
                                   where EF.Functions.Like(i.Nombre, $"%{streamerNombre}%")
                                   select i).ToListAsync();

            var h = await (from jota in dbContext.Videos
                     where jota.StreamerId == 1
                     select jota.Nombre).ToListAsync();


            foreach (var streamer in streamers)
            {
                Console.WriteLine($"{streamer.Id} - {streamer.Nombre}");
            }


        }

        public static async Task TrackingAndNotTracking(StreamerDbContext dbContext)
        {

            var streamerWithTracking = await dbContext!.Streamers!.FirstOrDefaultAsync(x => x.Id == 1);

            var streamerWithNoTracking = await dbContext!.Streamers!.AsNoTracking().FirstOrDefaultAsync(x => x.Id == 3);


            streamerWithTracking.Nombre = "Netflix Super";

            //no se puede actualizar ya que el objeto no se mantendra vigilado por EF
            streamerWithNoTracking.Nombre = "Amazon Plus";


            await dbContext!.SaveChangesAsync();

        }

        public static async Task AddNewStreamerWithVideo(StreamerDbContext dbContext)
        {
            var pantaya = new Streamer
            {
                Nombre = "Pantaya"
            };

            var hungerGames = new Video
            {
                Nombre = "Hunger Games",
                Streamer = pantaya
            };

            await dbContext.AddAsync(hungerGames);
            await dbContext.SaveChangesAsync();

        }

        public static async Task AddNewStreamerWithVideoId(StreamerDbContext dbContext)
        {
            var batmanForever = new Video
            {
                Nombre = "batman forever",
                StreamerId = 1002
            };

            await dbContext.AddAsync(batmanForever);
            await dbContext.SaveChangesAsync();

        }

        public static async Task AddNewActorWithVideo(StreamerDbContext dbContext)
        {
            var actor = new Actor
            {
                Nombre = "Brad",
                Apellido = "Pitt"
            };

            await dbContext.AddAsync(actor);
            await dbContext.SaveChangesAsync();

            var videoActor = new VideoActor
            {
                ActorId = actor.Id,
                VideoId = 1
            };

            await dbContext.AddAsync(videoActor);
            await dbContext.SaveChangesAsync();

        }

        public static async Task AddNewDirectorWithVideo(StreamerDbContext dbContext)
        {
            var director = new Director
            {
                Nombre = "Lorenzo",
                Apellido = "Basteri",
                VideoId = 1
            };

            await dbContext.AddAsync(director);
            await dbContext.SaveChangesAsync();
        }

        public static async Task MultipleEntitiesQuery(StreamerDbContext dbContext)
        {
            var videoWithActores = await dbContext!.Videos!.Include(q => q.Actores).FirstOrDefaultAsync(q => q.Id ==1);

            var actor = await dbContext!.Actores!.Select(q => q.Nombre).ToListAsync();


            var videoWithDirector = await dbContext!.Videos!
                                    .Where(q => q.Director != null)
                                    .Include(q => q.Director)
                                    .Select(q =>
                                       new
                                       {
                                           Director_Nombre_Completo = $"{q.Director.Nombre} {q.Director.Apellido}",
                                           Movie = q.Nombre
                                       }
                                     )
                                    .ToListAsync();


            foreach (var pelicula in videoWithDirector)
            {
                Console.WriteLine($"{pelicula.Movie}  - {pelicula.Director_Nombre_Completo} ");
            }


        }
    }

}
