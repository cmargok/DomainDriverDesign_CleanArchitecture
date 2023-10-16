using CleanArchitecture.Domain;

namespace CleanArchitecture.Application.Contracts.Persistence
{
    public interface IVideoRepository : IAsyncRepository<Video>
    { 
        public Task<Video> GetVideoByNombre(string nombre);

        public Task<IEnumerable<Video>> GetVideosByUsername(string userName);
    }
}