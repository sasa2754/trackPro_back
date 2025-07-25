using Moq;
using TrackPro.Application.Contracts.Persistence;
using TrackPro.Application.Features.Stations.Commands.CreateStation;
using TrackPro.Domain.Entities;
using Xunit;

namespace TrackPro.Application.UnitTests.Features.Stations.Commands
{
    public class CreateStationCommandHandlerTests
    {
        private readonly Mock<IStationRepository> _mockStationRepository;

        public CreateStationCommandHandlerTests()
        {
            _mockStationRepository = new Mock<IStationRepository>();
        }

        [Fact]
        public async Task Handle_Should_CallAddAsyncOnRepository_WhenCommandIsValid()
        {
            var handler = new CreateStationCommandHandler(_mockStationRepository.Object);

            var command = new CreateStationCommand { Name = "Test Station", Order = 99 };
            
            _mockStationRepository.Setup(repo => repo.AddAsync(It.IsAny<Station>()))
                .ReturnsAsync(new Station { Id = 5, Name = command.Name, Order = command.Order });

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsType<int>(result);
            Assert.Equal(5, result);

            _mockStationRepository.Verify(repo => repo.AddAsync(It.IsAny<Station>()), Times.Once);
        }
    }
}