using ConferenceRoomAppAPI.Data.Context;
using ConferenceRoomAppAPI.Data.Models;
using ConferenceRoomAppAPI.Services.Dtos.OrderDtos;
using ConferenceRoomAppAPI.Services.Interfaces;
using ConferenceRoomAppAPI.Services.Services;
using ConferenceRoomAppAPI.Services.Tools;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ConferenceRoomAppAPI.Services.Tests
{
    public class OrderServiceTest
    {
        [Fact]
        public void CreateOrderValidTest()
        {
            // Arrange
            var currentUserServiceMock = new Mock<ICurrentUserServices>();
            currentUserServiceMock.Setup(x => x.GetCurrentUser()).Returns("test-user-id");

            var dbContext = GetInMemoryDbContext();
            var discountRules = new List<IPriceDiscountRules>
            {
                new StandartDiscountRule(),
                new PeakTimeDiscountRule()
            };
            var priceCalculator = new PriceCalculator(discountRules);
            IHallServices hallsServices = new HallsServices(dbContext);
            IHallsServices hallServices = new HallsService(dbContext, hallsServices);
            IOrdersServices orderService = new OrderService(dbContext, hallServices, priceCalculator, hallsServices, currentUserServiceMock.Object);

            var createOrderDto = new CreateOrderDto
            {
                HallId = 1,
                StartTime = new TimeOnly(10, 0),
                EndTime = new TimeOnly(12, 0),
                Date = DateOnly.FromDateTime(DateTime.Now),
                ReservedServicesIds = new List<int> { 1 }
            };

            // Act
            var result = orderService.CreateOrderAsync(createOrderDto).Result;

            //
            Assert.Equal(100, result);
        }

        private ConferenceRoomContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ConferenceRoomContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var dbContext = new ConferenceRoomContext(options);

            dbContext.Halls.Add(new Halls { Id = 1, Name = "Hall 1", Capacity = 100, PricePerHour = 50, Services = new List<HallServices>() });

            dbContext.SaveChanges();

            return dbContext;
        }
    }
}
