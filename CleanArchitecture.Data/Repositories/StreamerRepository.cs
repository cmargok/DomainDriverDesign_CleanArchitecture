using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain;
using CleanArchitecture.Infrastructure.Persistence;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class StreamerRepository : BaseRepository<Streamer>, IStreamerRepository
    {
        private readonly StreamerDbContext _context;
        public StreamerRepository(StreamerDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
