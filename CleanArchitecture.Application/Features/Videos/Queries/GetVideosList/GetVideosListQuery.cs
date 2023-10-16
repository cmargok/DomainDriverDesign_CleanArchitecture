using MediatR;

namespace CleanArchitecture.Application.Features.Videos.Queries.GetVideosList
{
    public class GetVideosListQuery : IRequest<List<VideoVm>>
    {

        public string username { get; set; } = string.Empty;

        public GetVideosListQuery(string username)
        {
            this.username = username ?? throw new ArgumentNullException(nameof(username));
        }
    }
}
