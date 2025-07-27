using BusApi.Feature.Kids.Queries;
using BusApi.Models;
using BusApi.Repositories;
using Moq;

namespace Test.Feature.Kids.Queries
{
    public class GetAllKidsQueryHandlerTest
    {
        [Fact]
        public async Task Handle_Should_ReturnAllKidsSuccessfully()
        {
            var kidRepositoryMock = new Mock<IKidRepository>();

            var busRegistrationPlate1 = "AA123ZZ";
            var busRegistrationPlate2 = "CC987UU";
            var kid1 = new KidListResponse
            {
                Id = Guid.NewGuid(),
                Name = "Carlos",
                DocumentNumber = "46000001",
                BusRegistrationPlate = busRegistrationPlate1
            };
            var kid2 = new KidListResponse
            {
                Id = Guid.NewGuid(),
                Name = "Marta",
                DocumentNumber = "47000002",
                BusRegistrationPlate = busRegistrationPlate2
            };
            var kid3 = new KidListResponse
            {
                Id = Guid.NewGuid(),
                Name = "Juana",
                DocumentNumber = "48000003",
                BusRegistrationPlate = busRegistrationPlate1
            };
            var kid4 = new KidListResponse
            {
                Id = Guid.NewGuid(),
                Name = "Juan Pablo",
                DocumentNumber = "49000004",
                BusRegistrationPlate = null
            };
            var kids = new List<KidListResponse> { kid1, kid2, kid3, kid4 };

            kidRepositoryMock
                .Setup(r => r.GetAllListAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(kids));

            var request = new GetAllKidsQuery();
            var handler = new GetAllKidsQueryHandler(kidRepositoryMock.Object);

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(kids.Count, result.Count());
            Assert.Contains(result, k => k.Id == kid1.Id);
            Assert.Contains(result, k => k.Id == kid2.Id);
            Assert.Contains(result, k => k.Id == kid3.Id);
            Assert.Contains(result, k => k.Id == kid4.Id);

            kidRepositoryMock
                .Verify(r => r.GetAllListAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}