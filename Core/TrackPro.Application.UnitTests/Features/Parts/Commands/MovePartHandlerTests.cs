using Moq;
using TrackPro.Application.Contracts.Persistence;
using TrackPro.Application.Features.Parts.Commands.MovePart;
using TrackPro.Domain.Entities;
using Xunit;
using TrackPro.Application.Exceptions;
using System.Net;

namespace TrackPro.Application.UnitTests.Features.Parts.Commands
{
    public class MovePartCommandHandlerTests
    {
        private readonly Mock<IPartRepository> _mockPartRepo;
        private readonly Mock<IStationRepository> _mockStationRepo;
        private readonly Mock<IMovementRepository> _mockMovementRepo;
        private readonly MovePartCommandHandler _handler;

        public MovePartCommandHandlerTests()
        {
            _mockPartRepo = new Mock<IPartRepository>();
            _mockStationRepo = new Mock<IStationRepository>();
            _mockMovementRepo = new Mock<IMovementRepository>();
            
            _handler = new MovePartCommandHandler(
                _mockPartRepo.Object, 
                _mockStationRepo.Object, 
                _mockMovementRepo.Object);
        }

        [Fact]
        public async Task Handle_Should_MovePartToNextStation_WhenMoveIsValid()
        {
            var part = new Part("P001", "Test Part");
            var currentStation = new Station { Id = 1, Name = "Recebimento", Order = 1 };
            var nextStation = new Station { Id = 2, Name = "Montagem", Order = 2 };
            var command = new MovePartCommand { PartCode = "P001", Responsible = "Tester" };

            _mockPartRepo.Setup(r => r.GetByCodeAsync(command.PartCode)).ReturnsAsync(part);
            _mockStationRepo.Setup(r => r.GetByIdAsync(currentStation.Id)).ReturnsAsync(currentStation);
            _mockStationRepo.Setup(r => r.GetByOrderAsync(currentStation.Order + 1)).ReturnsAsync(nextStation);

            await _handler.Handle(command, CancellationToken.None);

            _mockPartRepo.Verify(r => r.UpdateAsync(It.IsAny<Part>()), Times.Once);
            _mockMovementRepo.Verify(r => r.AddAsync(It.IsAny<Movement>()), Times.Once);
            Assert.Equal(2, part.CurrentStationId);
        }

        [Fact]
        public async Task Handle_Should_FinalizePart_WhenMovingFromLastStation()
        {
            var part = new Part("P001", "Test Part");
            var lastStation = new Station { Id = 3, Name = "Inspeção Final", Order = 3 };
            part.MoveToNextStation(new Station { Id = 2, Name = "Montagem", Order = 2 });
            part.MoveToNextStation(lastStation);
            
            var command = new MovePartCommand { PartCode = "P001", Responsible = "Tester" };

            _mockPartRepo.Setup(r => r.GetByCodeAsync(command.PartCode)).ReturnsAsync(part);
            _mockStationRepo.Setup(r => r.GetByIdAsync(lastStation.Id)).ReturnsAsync(lastStation);
           _mockStationRepo.Setup(r => r.GetByOrderAsync(lastStation.Order + 1)).ReturnsAsync(default(Station));

            await _handler.Handle(command, CancellationToken.None);

            _mockPartRepo.Verify(r => r.UpdateAsync(It.IsAny<Part>()), Times.Once);
            Assert.Equal("Finalizada", part.Status);
        }
        
        [Fact]
        public async Task Handle_Should_ThrowException_WhenPartIsAlreadyFinished()
        {
            var part = new Part("P001", "Test Part");
            part.FinishProcess();
            var command = new MovePartCommand { PartCode = "P001", Responsible = "Tester" };

            _mockPartRepo.Setup(r => r.GetByCodeAsync(command.PartCode)).ReturnsAsync(part);

            var exception = await Assert.ThrowsAsync<ApiException>(() => 
                _handler.Handle(command, CancellationToken.None));

            Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
            Assert.Equal("Cannot move a part that is already finished.", exception.Message);
        }
    }
}