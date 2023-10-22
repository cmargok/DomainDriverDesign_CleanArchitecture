using AutoMapper;
using CleanArchitecture.Application.Contracts.UnitOfWork;
using CleanArchitecture.Application.Features.Videos.Queries.GetVideosList;
using CleanArchitecture.Application.Tests.Mocks;
using CleanArchitecture.Application.Utils.Mappings;
using FluentAssertions;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.Application.Tests.Features.Video.Queries
{
    public class GetVideosListQueryHandlerXUnitTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _unitOfWork;

        public GetVideosListQueryHandlerXUnitTests()
        {
            _unitOfWork = MockUnitOfWork.GetUnitOfWorkDBINMEMORY();

            var mapperConfig = new MapperConfiguration(
                c => c.AddProfile<MappingProfile>());

            _mapper = mapperConfig.CreateMapper();           
        }

        [Fact]
        public async Task Handle_whenIsCalled_shouldReturnVideosList() 
        {
            var handler = new GetVideosListQueryHandler(_mapper,_unitOfWork.Object);

            var request = new GetVideosListQuery("systems");

            var result = await handler.Handle(request, CancellationToken.None);

            result.ShouldBeOfType(typeof(List<VideoVm>));
            result.ShouldBeOfType<List<VideoVm>>();

            result.Should().BeOfType<List<VideoVm>>();
            result.Should().BeOfType(typeof(List<VideoVm>));
        }


        [Fact]
        public async Task Handle_whenIsCalled_shouldReturnVideosList_WITHdATABASEINMEMORY()
        {
            var handler = new GetVideosListQueryHandler(_mapper, _unitOfWork.Object);

            var request = new GetVideosListQuery("systems");

            var result = await handler.Handle(request, CancellationToken.None);

            result.ShouldBeOfType(typeof(List<VideoVm>));
            result.ShouldBeOfType<List<VideoVm>>();

            result.Should().BeOfType<List<VideoVm>>();
            result.Should().BeOfType(typeof(List<VideoVm>));

            result.Count.ShouldBe(1);
        }
    }
}
