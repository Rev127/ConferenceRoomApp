using ConferenceRoomAppAPI.Services.Interfaces;
using ConferenceRoomAppAPI.Services.Tools;
using ConferenceRoomAppAPI.Services.Dtos.OrderDtos;
using ConferenceRoomAppAPI.Services.Dtos;
using ConferenceRoomAppAPI.Services.Dtos.HallDtos;
using ConferenceRoomAppAPI.Services.Dtos.HallServicesDtos;

namespace ConferenceRoomAppAPI.Services.Tests.PriceCalculatorTests
{
    public class PriseCalculatorTests
    {
        [Fact]
        public void CalculatePrice_ShouldReturnCorrectPrice_WhenGivenValidInput()
        {
            // Arrange
            var basePrice = 100m;
            var timeSlot = new TimeSlot(new TimeOnly(11, 30), new TimeOnly(12, 30));
            var discountRules = new List<IPriceDiscountRules>
            {
                new StandartDiscountRule(),
                new PeakTimeDiscountRule()
            };
            var priceCalculator = new PriceCalculator(discountRules);
            // Act
            var finalPrice = priceCalculator.CalculateTotalPrice(new PriceCalculateDto
            {
                ReservedServices = new List<GetHallServicesDto>(),
                Hall = new GetHallDto { PricePerHour = basePrice },
                StartTime = timeSlot.StartTime,
                EndTime = timeSlot.EndTime
            });
            // Assert
            Assert.Equal(107.5m, finalPrice);
        }

        [Fact]
        public void CalculatePrice_ShouldReturnCorrectPrice_WhenGivenValidInput2()
        {
            // Arrange
            var basePrice = 100m;
            var timeSlot = new TimeSlot(new TimeOnly(9, 0), new TimeOnly(17, 0));
            var discountRules = new List<IPriceDiscountRules>
            {
                new StandartDiscountRule(),
                new PeakTimeDiscountRule()
            };
            var priceCalculator = new PriceCalculator(discountRules);
            // Act
            var finalPrice = priceCalculator.CalculateTotalPrice(new PriceCalculateDto
            {
                ReservedServices = new List<GetHallServicesDto>(),
                Hall = new GetHallDto { PricePerHour = basePrice },
                StartTime = timeSlot.StartTime,
                EndTime = timeSlot.EndTime
            });
            // Assert
            Assert.Equal(830m, finalPrice);
        }

        [Fact]
        public void CalculatePrice_ShouldReturnCorrectPrice_WhenGivenValidInput3()
        {
            // Arrange
            var basePrice = 100m;
            var timeSlot = new TimeSlot(new TimeOnly(11, 13), new TimeOnly(14, 15));
            var discountRules = new List<IPriceDiscountRules>
            {
                new StandartDiscountRule(),
                new PeakTimeDiscountRule()
            };
            var priceCalculator = new PriceCalculator(discountRules);
            // Act
            var finalPrice = priceCalculator.CalculateTotalPrice(new PriceCalculateDto
            {
                ReservedServices = new List<GetHallServicesDto>(),
                Hall = new GetHallDto { PricePerHour = basePrice },
                StartTime = timeSlot.StartTime,
                EndTime = timeSlot.EndTime
            });
            // Assert
            Assert.Equal(333.33m, finalPrice);
        }

        [Fact]
        public void CalculatePrice_ShouldReturnCorrectPrice_WhenGivenValidInput4()
        {
            // Arrange
            var basePrice = 100m;
            var timeSlot = new TimeSlot(new TimeOnly(11, 13), new TimeOnly(14, 15));
            var discountRules = new List<IPriceDiscountRules>
            {
                new StandartDiscountRule(),
                new PeakTimeDiscountRule()
            };
            var priceCalculator = new PriceCalculator(discountRules);

            var reservedServices = new List<GetHallServicesDto>
            {
                new GetHallServicesDto { Id = 1, Name = "Service 1", Price = 50m }
            };

            // Act
            var finalPrice = priceCalculator.CalculateTotalPrice(new PriceCalculateDto
            {
                ReservedServices = reservedServices,
                Hall = new GetHallDto { PricePerHour = basePrice },
                StartTime = timeSlot.StartTime,
                EndTime = timeSlot.EndTime
            });

            // Assert
            Assert.Equal(383.33m, finalPrice);
        }
    }
}
