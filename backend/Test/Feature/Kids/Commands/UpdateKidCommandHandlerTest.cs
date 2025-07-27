using BusApi.Domain;
using BusApi.Feature.Kids.Commands;
using BusApi.Repositories;
using MediatR;
using Moq;

namespace Test.Feature.Kids.Commands
{
    public class UpdateKidCommandHandlerTest
    {
        [Fact]
        public async Task Handle_Should_UpdateKidSuccessfully()
        {
            var kidRepositoryMock = new Mock<IKidRepository>();

            var id = Guid.NewGuid();
            var kid = new Kid
            {
                Id = id,
                Name = "Carlos",
                DocumentNumber = "12345678",
                Bus = new Bus
                {
                    Id = new Guid(),
                    RegistrationPlate = "AA123ZZ",
                }
            };
            kidRepositoryMock
                .Setup(r => r.GetByIdWithBusAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(kid));

            kidRepositoryMock
                .Setup(r => r.UpdateAsync(It.IsAny<Kid>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var request = new UpdateKidCommand(id, "36000022", "Juan Pablo");
            var handler = new UpdateKidCommandHandler(kidRepositoryMock.Object);

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.Equal(Unit.Value, result);

            kidRepositoryMock
                .Verify(r => r.GetByIdWithBusAsync(id, It.IsAny<CancellationToken>()), Times.Once);
            kidRepositoryMock
                .Verify(r => r.UpdateAsync(kid, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_ThrowException_When_KidNotFound()
        {
            var kidRepositoryMock = new Mock<IKidRepository>();

            var id = Guid.NewGuid();
            Kid kid = null;
            kidRepositoryMock
                .Setup(r => r.GetByIdWithBusAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(kid));

            var request = new UpdateKidCommand(id, "36000022", "Juan Pablo");
            var handler = new UpdateKidCommandHandler(kidRepositoryMock.Object);

            var exception = await Assert.ThrowsAsync<Exception>(() => handler.Handle(request, CancellationToken.None));
            Assert.Contains("Kid", exception.Message);
            Assert.Contains(id.ToString(), exception.Message);
            Assert.Contains("not found", exception.Message);

            kidRepositoryMock
                .Verify(r => r.GetByIdWithBusAsync(id, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}