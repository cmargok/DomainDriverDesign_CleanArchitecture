
using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Contracts.UnitOfWork;
using MediatR;

namespace CleanArchitecture.Application.Features.Videos.Queries.GetVideosList
{
    public class GetVideosListQueryHandler : IRequestHandler<GetVideosListQuery,List<VideoVm>>
    {
      //  private readonly IVideoRepository _videoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetVideosListQueryHandler(
          //  IVideoRepository videoRepository, 
            IMapper mapper, IUnitOfWork unitOfWork)
        {
           // _videoRepository = videoRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<VideoVm>> Handle(GetVideosListQuery request, CancellationToken cancellationToken)
        {
            var videoList = await _unitOfWork.VideoRepository.GetVideosByUsername(request.username);

            return _mapper.Map<List<VideoVm>>(videoList);
        }
    }
}
