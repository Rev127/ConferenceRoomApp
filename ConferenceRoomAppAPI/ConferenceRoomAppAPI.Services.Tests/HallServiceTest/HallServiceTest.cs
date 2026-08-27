using ConferenceRoomAppAPI.Services.Dtos.HallDtos;
using ConferenceRoomAppAPI.Services.Dtos.HallServicesDtos;
using ConferenceRoomAppAPI.Services.Interfaces;
using ConferenceRoomAppAPI.Services.Services;
using ConferenceRoomAppAPI.Data.Models;
using ConferenceRoomAppAPI.Data.Context;
using Microsoft.EntityFrameworkCore;
using ConferenceRoomAppAPI.Services.Exceptions.HallServicesExceptions;

namespace ConferenceRoomAppAPI.Services.Tests
{
    public class HallServiceTest
    {
        [Fact]
        public async Task CreateHallValidTest()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            IHallServices hallsServices = new HallsServices(dbContext);
            IHallsServices hallServices = new HallsService(dbContext, hallsServices);
            CreateHallDto hallCreateDto = new CreateHallDto
            {
                Name = "Test Hall",
                Capacity = 100,
                PricePerHour = 100,
                HallServices = new List<CreateHallServicesDto>
                {
                    new CreateHallServicesDto { Name= "Service 1", Price = 10 }
                }
            };

            // Act
            var result = await hallServices.CreateHallAsync(hallCreateDto);
            var createdHall = dbContext.Halls.FirstOrDefault(h => h.Name == "Test Hall");

            // Assert
            Assert.Equal("Test Hall", createdHall.Name);
        }

        [Fact]
        public async Task CreateHallInvalidTest()
        {

            // Arrange
            var dbContext = GetInMemoryDbContext();
            IHallServices hallsServices = new HallsServices(dbContext);
            IHallsServices hallServices = new HallsService(dbContext, hallsServices);
            CreateHallDto hallCreateDto = new CreateHallDto
            {
                Name = "Test Hall",
                Capacity = 100,
                PricePerHour = 100,
                HallServices = new List<CreateHallServicesDto>
                {
                    new CreateHallServicesDto { Name= "Service 2", Price = 10 }
                }
            };

            // Act
            Func<Task> act = async () => await hallServices.CreateHallAsync(hallCreateDto);

            // Assert
            await Assert.ThrowsAsync<HallServicesNotExistsException>(act);
        }

        [Fact]
        public async Task UpdateHallTest()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            IHallServices hallsServices = new HallsServices(dbContext);
            IHallsServices hallServices = new HallsService(dbContext, hallsServices);

            var hall = new Halls
            {
                Name = "Test Hall",
                Capacity = 200,
                PricePerHour = 100
            };

            await dbContext.Halls.AddAsync(hall);
            await dbContext.SaveChangesAsync();

            UpdateHallDto hallCreateDto = new UpdateHallDto
            {
                Id = hall.Id,
                Name = "Updated Hall",
                Capacity = 100,
                HallServices = new List<GetHallServicesDto>
                {
                    new GetHallServicesDto { Id = 1, Name= "Service 1", Price = 10 }
                }
            };

            // Act
            await hallServices.UpdateHallAsync(hallCreateDto);
            var createdHall = dbContext.Halls.FirstOrDefault(h => h.Name == hallCreateDto.Name);
            // Assert
            Assert.Equal(hallCreateDto.Name, createdHall.Name);
            Assert.Equal(hallCreateDto.Capacity, createdHall.Capacity);
            Assert.Equal(hallCreateDto.HallServices.Count, createdHall.Services.Count);
        }

        private ConferenceRoomContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ConferenceRoomContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var dbContext = new ConferenceRoomContext(options);

            dbContext.HallServices.Add(new HallServices { Id = 1, Name = "Service 1", Price = 10 });

            dbContext.SaveChanges();

            return dbContext;
        }
    }
}
