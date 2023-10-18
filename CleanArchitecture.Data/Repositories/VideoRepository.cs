using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain;
using CleanArchitecture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
namespace CleanArchitecture.Infrastructure.Repositories
{
    public class VideoRepository : BaseRepository<Video>, IVideoRepository
    {
        private readonly StreamerDbContext _context;
        public VideoRepository(StreamerDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Video> GetVideoByNombre(string nombre)
        {
           var video = await  _context.Videos!.Where(v => v.Nombre == nombre).SingleOrDefaultAsync();

            return video!;
        }

        public async  Task<IEnumerable<Video>> GetVideosByUsername(string userName)
        {
            var video = await _context.Videos!.Where(v => v.CreatedBy == userName).ToListAsync();

            return video!;
        }
    }
}
