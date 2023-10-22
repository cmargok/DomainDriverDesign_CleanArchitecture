using CleanArchitecture.Application.Contracts.UnitOfWork;
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

        public static Mock<IUnitOfWork> GetUnitOfWorkDBINMEMORY()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            var mockVideoRepository = MockVideoRepository.GetVideoRepositoryWithDatabaseInMemory();

            mockUnitOfWork.Setup(r => r.VideoRepository).Returns(mockVideoRepository.Object);

            return mockUnitOfWork;

        }
    }
}
