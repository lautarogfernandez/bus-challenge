using BusApi.Domain;
using BusApi.Feature.Drivers.Queries;
using BusApi.Repositories;
using Moq;

namespace Test.Feature.Drivers.Queries
{
    public class GetDriverByIdQueryHandlerTest
    {
        [Fact]
        public async Task Handle_Should_ReturnDriverSuccessfully()
        {
            var driverRepositoryMock = new Mock<IDriverRepository>();

            var id = Guid.NewGuid();
            var busRegistrationPlate = "AA123ZZ";
            var kid1 = new Kid
            {
                Id = Guid.NewGuid(),
                Name = "Marta",
                DocumentNumber = "47000002"
            };
            var kid2 = new Kid
            {
                Id = Guid.NewGuid(),
                Name = "Juana",
                DocumentNumber = "48000003"
            };
            var driver = new Driver
            {
                Id = id,
                Name = "Juan Pablo",
                DocumentNumber = "12345678",
                Bus = new Bus
                {
                    Id = Guid.NewGuid(),
                    RegistrationPlate = busRegistrationPlate,
                    DriverId = id,
                    Kids = [kid1, kid2]
                },

            };
            driverRepositoryMock
                .Setup(r => r.GetByIdWithBusAsync(id, It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(driver));

            var request = new GetDriverByIdQuery(id);
            var handler = new GetDriverByIdQueryHandler(driverRepositoryMock.Object);

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(driver.Id, result.Id);
            Assert.Equal(driver.DocumentNumber, result.DocumentNumber);
            Assert.Equal(driver.Name, result.Name);
            Assert.Equal(driver.Bus.RegistrationPlate, result.BusRegistrationPlate);

            driverRepositoryMock
                .Verify(r => r.GetByIdWithBusAsync(id, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_ReturnNull_When_DriverNotFound()
        {
            var driverRepositoryMock = new Mock<IDriverRepository>();

            var id = Guid.NewGuid();
            Driver driver = null;
            driverRepositoryMock
                .Setup(r => r.GetByIdWithBusAsync(id, It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(driver));

            var request = new GetDriverByIdQuery(id);
            var handler = new GetDriverByIdQueryHandler(driverRepositoryMock.Object);

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.Null(result);

            driverRepositoryMock
                .Verify(r => r.GetByIdWithBusAsync(id, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}