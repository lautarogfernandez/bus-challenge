using BusApi.Domain;
using BusApi.Feature.Buses.Commands;
using BusApi.Repositories;
using Moq;

namespace Test.Feature.Buses.Commands
{
    public class CreateBusCommandHandlerTest
    {
        [Fact]
        public async Task Handle_Should_CreateBusSuccessfully()
        {
            var kidRepositoryMock = new Mock<IKidRepository>();
            var busRepositoryMock = new Mock<IBusRepository>();

            var kid1 = new Kid { Id = Guid.NewGuid(), Name = "Juan", DocumentNumber = "49000123" };
            var kid2 = new Kid { Id = Guid.NewGuid(), Name = "Ana", DocumentNumber = "50000987" };

            var registratioPlate = "AA123ZZ";
            var request = new CreateBusCommand(
               registratioPlate,
               Guid.NewGuid(),
               [kid1.Id, kid2.Id]
           );

            kidRepositoryMock
                .Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync([kid1, kid2]);

            var newId = new Guid("d7868e4f-4401-44b3-a38e-373d7a77a152");
            Bus? createdBus = null;
            busRepositoryMock
                .Setup(r => r.CreateAsync(It.IsAny<Bus>(), It.IsAny<CancellationToken>()))
                .Callback<Bus, CancellationToken>((bus, _) =>
                {
                    bus.Id = newId;
                    createdBus = bus;
                })
                .Returns(Task.CompletedTask);

            var handler = new CreateBusCommandHandler(busRepositoryMock.Object, kidRepositoryMock.Object);

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.Equal(newId, result);
            Assert.NotNull(createdBus);
            Assert.Equal(registratioPlate, createdBus.RegistrationPlate);
            Assert.Equal(request.DriverId, createdBus.DriverId);
            Assert.Equal(2, createdBus.Kids.Count);
            Assert.Contains(createdBus.Kids, k => k.Id == kid1.Id);
            Assert.Contains(createdBus.Kids, k => k.Id == kid2.Id);

            busRepositoryMock
                .Verify(r => r.CreateAsync(It.IsAny<Bus>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}