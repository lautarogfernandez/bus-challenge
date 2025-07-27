using BusApi.Domain;
using BusApi.Feature.Drivers.Commands;
using BusApi.Repositories;
using MediatR;
using Moq;

namespace Test.Feature.Drivers.Commands
{
    public class DeleteDriverCommandHandlerTest
    {
        [Fact]
        public async Task Handle_Should_DeleteDriverSuccessfully()
        {
            var driverRepositoryMock = new Mock<IDriverRepository>();

            var id = Guid.NewGuid();
            var driver = new Driver
            {
                Id = id,
                Name = "Carlos",
                DocumentNumber = "12345678",
                Bus = null
            };
            driverRepositoryMock
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(driver));

            driverRepositoryMock
                .Setup(r => r.DeleteAsync(It.IsAny<Driver>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var request = new DeleteDriverCommand(id);
            var handler = new DeleteDriverCommandHandler(driverRepositoryMock.Object);

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.Equal(Unit.Value, result);

            driverRepositoryMock
                .Verify(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()), Times.Once);
            driverRepositoryMock
                .Verify(r => r.DeleteAsync(driver, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_ThrowException_When_DriverNotFound()
        {
            var driverRepositoryMock = new Mock<IDriverRepository>();

            var id = Guid.NewGuid();
            Driver driver = null;
            driverRepositoryMock
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(driver));

            var request = new DeleteDriverCommand(id);
            var handler = new DeleteDriverCommandHandler(driverRepositoryMock.Object);

            var exception = await Assert.ThrowsAsync<Exception>(() => handler.Handle(request, CancellationToken.None));
            Assert.Contains("Driver", exception.Message);
            Assert.Contains(id.ToString(), exception.Message);
            Assert.Contains("not found", exception.Message);

            driverRepositoryMock
                .Verify(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}