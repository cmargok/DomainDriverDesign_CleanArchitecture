using AutoFixture;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Contracts.UnitOfWork;
using CleanArchitecture.Domain;
using Moq;

namespace CleanArchitecture.Application.Tests.Mocks
{
    public class MockUnitOfWork
    {
        public static Mock<IUnitOfWork> GetUnitOfWork()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            var mockVideoRepository = MockVideoRepository.GetVideoRepository();

            mockUnitOfWork.Setup(r => r.VideoRepository).Returns(mockVideoRepository.Object);

            return mockUnitOfWork;

        }
    }


    public static class MockVideoRepository
    {
        public static Mock<IVideoRepository> GetVideoRepository()
        {
            var fixture = new Fixture();
            //solucionamos el problema de referencias circular
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var videos = fixture.CreateMany<Video>().ToList();

            var mockRepository = new Mock<IVideoRepository>();

            mockRepository.Setup( r => r.GetAllAsync()).ReturnsAsync(videos);

            return mockRepository;
        }
    }
}
